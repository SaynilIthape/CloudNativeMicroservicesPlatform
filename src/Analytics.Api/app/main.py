from fastapi import FastAPI,Depends, HTTPException
from sqlalchemy import text
from sqlalchemy.orm import Session

from app.dependencies import get_db
from app.config import settings
from sqlalchemy import func, select
from app.models import SalesRecord
from app.schema import SalesSummaryResponse

from app.schema import SalesRecordCreate, SalesRecordResponse



app = FastAPI(
    title=settings.app_name,
    version="1.0.0",
    description="Cloud-native Analytics API"
)

@app.get("/")
def root():
    return {
        "message": "CloudNative Analytics API",
        "version": "1.0.0"
    }

@app.get("/health")
def health_check():
    return {
        "status": "healthy",
        "service": "analytics-api",
        "environment": settings.app_environment
    }


@app.get("/health/database")
def health_check_database(db: Session = Depends(get_db)):
    try:
        result = db.execute(text("SELECT 1"))
        return {"status": "healthy", "database_result": result.scalar()}
    except Exception as e:
        raise HTTPException(status_code=503, detail=f"Database health check failed: {str(e)}")  


@app.get("/analytics/summary",response_model=SalesSummaryResponse)
def get_sales_summary(db: Session = Depends(get_db)):
    query = select(
        func.count(SalesRecord.id).label("total_sales"),
        func.coalesce(func.sum(SalesRecord.quantity), 0).label("total_quantity"),
        func.coalesce(
            func.sum(SalesRecord.quantity * SalesRecord.unit_price),
            0
        ).label("total_revenue")
    )
   
    result = db.execute(query).mappings().one()

    return SalesSummaryResponse(
        total_sales=result["total_sales"],
        total_quantity=result["total_quantity"],
        total_revenue=result["total_revenue"]   
    )


@app.post("/sales", response_model=SalesRecordResponse, status_code=201)
def create_sales_record(sales_record: SalesRecordCreate, db: Session = Depends(get_db)):
    new_record =SalesRecord(**sales_record.model_dump())
    db.add(new_record)
    db.commit()
    db.refresh(new_record)
    return new_record