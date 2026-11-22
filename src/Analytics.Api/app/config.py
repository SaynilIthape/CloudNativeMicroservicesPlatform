
from pydantic_settings import BaseSettings, SettingsConfigDict


class Settings(BaseSettings):

    app_name: str = "CloudNative Analytics API"
    app_environment: str = "Development"

    # Azure SQL
    azure_sql_server: str | None = None
    azure_sql_database: str | None = None
    azure_sql_driver: str = "ODBC Driver 18 for SQL Server"
    azure_client_id: str | None = None

    # Azure AI Foundry / Azure OpenAI
    azure_openai_endpoint: str | None = None
    azure_openai_deployment: str | None = None
    azure_openai_api_version: str | None = None

    # MCP
    mcp_server_name: str = "CloudNative Analytics MCP"

    model_config = SettingsConfigDict(
        env_file=".env",
        extra="ignore",
        case_sensitive=False
    )


settings = Settings()
