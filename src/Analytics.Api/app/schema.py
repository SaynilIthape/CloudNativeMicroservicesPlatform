
from datetime import date
from decimal import Decimal

from pydantic import BaseModel, ConfigDict, Field


class SalesRecordCreate(BaseModel):
    product_name: str = Field(min_length=1, max_length=150)
    category: str = Field(min_length=1, max_length=100)
    region: str = Field(min_length=1, max_length=100)
    quantity: int = Field(gt=0)
    unit_price: Decimal = Field(gt=0, max_digits=18, decimal_places=2)
    sale_date: date


class SalesRecordResponse(SalesRecordCreate):
    id: int

    model_config = ConfigDict(from_attributes=True)


class SalesSummaryResponse(BaseModel):
    total_sales: int
    total_quantity: int
    total_revenue: Decimal
