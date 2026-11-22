import struct

import pyodbc
from azure.identity import DefaultAzureCredential
from sqlalchemy import create_engine, event
from sqlalchemy.engine import URL

from app.config import settings

credential = DefaultAzureCredential(
    managed_identity_client_id=settings.azure_client_id
)
engine = None

def get_engine():
    global engine

    if engine is not None:
        return engine

    if not settings.azure_sql_server or not settings.azure_sql_database:
        raise ValueError("Azure SQL Server and Database must be set in the configuration.") 

    connection_string =  (
        f"Driver={settings.azure_sql_driver};"
        f"Server={settings.azure_sql_server};"
        f"Database={settings.azure_sql_database};"
        "Encrypt=yes;"
        "TrustServerCertificate=no;"
        "Connection Timeout=30;"
    )

    connection_url = URL.create("mssql+pyodbc", query={"odbc_connect": connection_string})
   
    engine = create_engine(connection_url, pool_pre_ping=True)

    @event.listens_for(engine, "do_connect")
    def provide_token(dialect, conn_rec, cargs, cparams):
        token = credential.get_token(
            "https://database.windows.net/.default"
        ).token

        token_bytes = token.encode("utf-16-le")

        token_struct = struct.pack(
            f"<I{len(token_bytes)}s",
            len(token_bytes),
            token_bytes
        )

        cparams["attrs_before"] = {
            1256: token_struct
        }
    return engine