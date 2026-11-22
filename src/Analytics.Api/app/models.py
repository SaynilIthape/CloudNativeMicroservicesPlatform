
from sqlalchemy import Column, Date, Integer, Numeric, String
from sqlalchemy.orm import DeclarativeBase


class Base(DeclarativeBase):
    pass


class SalesRecord(Base):
    __tablename__ = "sales_records"

    id = Column(Integer, primary_key=True, index=True)
    product_name = Column(String(150), nullable=False)
    category = Column(String(100), nullable=False)
    region = Column(String(100), nullable=False)
    quantity = Column(Integer, nullable=False)
    unit_price = Column(Numeric(18, 2), nullable=False)
    sale_date = Column(Date, nullable=False)
