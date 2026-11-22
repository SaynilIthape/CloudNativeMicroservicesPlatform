SELECT DB_NAME() AS DatabaseName,
       SUSER_SNAME() AS LoginName;

       CREATE TABLE sales_records
(
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_name NVARCHAR(100) NOT NULL,
    category NVARCHAR(100) NOT NULL,
    region NVARCHAR(100) NOT NULL,
    quantity INT NOT NULL,
    unit_price DECIMAL(18,2) NOT NULL,
    sale_date DATETIME2 NOT NULL DEFAULT GETDATE()
);


INSERT INTO sales_records
    (product_name, category, region, quantity, unit_price, sale_date)
VALUES
    ('Laptop', 'Electronics', 'Pune', 5, 65000, GETDATE()),
    ('Monitor', 'Electronics', 'Mumbai', 10, 12000, GETDATE()),
    ('Keyboard', 'Accessories', 'Pune', 20, 1500, GETDATE()),
    ('Mouse', 'Accessories', 'Bangalore', 15, 800, GETDATE()),
    ('Laptop', 'Electronics', 'Hyderabad', 3, 65000, GETDATE());

    select *from sales_records

    CREATE USER [cloudnative-analytics-identity]
FROM EXTERNAL PROVIDER;

grant select, insert on
dbo.sales_records
to [cloudnative-analytics-identity]