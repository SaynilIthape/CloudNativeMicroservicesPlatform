
from collections.abc import Generator

from sqlalchemy.orm import Session, sessionmaker

from app.database import get_engine


def get_db() -> Generator[Session, None, None]:
    engine = get_engine()

    SessionLocal = sessionmaker(
        bind=engine,
        autoflush=False,
        autocommit=False
    )

    db = SessionLocal()

    try:
        yield db
    finally:
        db.close()
