using System;
using System.Collections.Generic;
using DemmacsAPIv2.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DemmacsAPIv2.Data
{
    public partial class DemmacsdbContext : DbContext
    {
        private readonly IConfiguration _config;
        public DemmacsdbContext()
        {
        }

        public DemmacsdbContext(DbContextOptions<DemmacsdbContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Delivery> Deliveries { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Orderitem> Orderitems { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Postalcode> Postalcodes { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductColor> ProductColors { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Stock> Stocks { get; set; } = null!;
        public virtual DbSet<StockProduct> StockProducts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL(_config.GetConnectionString("Demmacs"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.HasIndex(e => e.CustomerId, "CustomerID");

                entity.HasIndex(e => e.ProductId, "ProductID");

                entity.Property(e => e.CartId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CartID");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CustomerID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductID");

                entity.Property(e => e.Quantity)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Cart_ibfk_2");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Cart_ibfk_1");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(45);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("color");

                entity.Property(e => e.ColorId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ColorID");

                entity.Property(e => e.ColorName).HasMaxLength(20);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.Property(e => e.CountryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CountryID");

                entity.Property(e => e.CountryCode).HasMaxLength(10);

                entity.Property(e => e.CountryName).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.CountryId, "CountryID");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CustomerID");

                entity.Property(e => e.CountryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CountryID");

                entity.Property(e => e.CustomerAddr).HasMaxLength(65);

                entity.Property(e => e.CustomerEmail).HasMaxLength(65);

                entity.Property(e => e.CustomerFname)
                    .HasMaxLength(45)
                    .HasColumnName("CustomerFName");

                entity.Property(e => e.CustomerPhone).HasColumnType("bigint(20)");

                entity.Property(e => e.CustomerSname)
                    .HasMaxLength(45)
                    .HasColumnName("CustomerSName");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(12)
                    .IsFixedLength();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Customer_ibfk_1");

                entity.HasOne(d => d.PostalCodeNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.PostalCode)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Customer_ibfk_2");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.ToTable("delivery");

                entity.HasIndex(e => e.CountryId, "CountryID");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.Property(e => e.DeliveryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("DeliveryID");

                entity.Property(e => e.CountryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CountryID");

                entity.Property(e => e.DeliveredDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DeliveryAddr).HasMaxLength(65);

                entity.Property(e => e.DeliveryFee).HasPrecision(5);

                entity.Property(e => e.DispatchedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.EstDeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(12)
                    .IsFixedLength();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Deliveries)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Delivery_ibfk_1");

                entity.HasOne(d => d.PostalCodeNavigation)
                    .WithMany(p => p.Deliveries)
                    .HasForeignKey(d => d.PostalCode)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Delivery_ibfk_2");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.HasIndex(e => e.CountryId, "CountryID");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.Property(e => e.DepartmentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("DepartmentID");

                entity.Property(e => e.CountryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CountryID");

                entity.Property(e => e.DepartmentAddr).HasMaxLength(65);

                entity.Property(e => e.DepartmentEmail).HasMaxLength(65);

                entity.Property(e => e.DepartmentName).HasMaxLength(45);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(12)
                    .IsFixedLength();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Department_ibfk_1");

                entity.HasOne(d => d.PostalCodeNavigation)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.PostalCode)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Department_ibfk_2");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasIndex(e => e.CountryId, "CountryID");

                entity.HasIndex(e => e.DepartmentId, "DepartmentID");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.HasIndex(e => e.RoleId, "RoleID");

                entity.Property(e => e.EmployeeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.CountryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CountryID");

                entity.Property(e => e.DepartmentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("DepartmentID");

                entity.Property(e => e.EmployeeAddr).HasMaxLength(65);

                entity.Property(e => e.EmployeeEmail).HasMaxLength(65);

                entity.Property(e => e.EmployeeFname)
                    .HasMaxLength(45)
                    .HasColumnName("EmployeeFName");

                entity.Property(e => e.EmployeePhone).HasColumnType("bigint(20)");

                entity.Property(e => e.EmployeeSalary).HasPrecision(10);

                entity.Property(e => e.EmployeeSname)
                    .HasMaxLength(45)
                    .HasColumnName("EmployeeSName");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.HireDate).HasColumnType("date");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(12)
                    .IsFixedLength();

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("RoleID")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Employee_ibfk_2");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Employee_ibfk_1");

                entity.HasOne(d => d.PostalCodeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PostalCode)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Employee_ibfk_5");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Employee_ibfk_4");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("login");

                entity.HasIndex(e => e.CustomerId, "CustomerID");

                entity.HasIndex(e => e.EmployeeId, "EmployeeID");

                entity.Property(e => e.LoginId)
                    .HasColumnType("int(11)")
                    .HasColumnName("LoginID");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CustomerID")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Email).HasMaxLength(65);

                entity.Property(e => e.EmployeeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("EmployeeID")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.UserType)
                    .HasColumnType("bit(2)")
                    .HasDefaultValueSql("b'0'");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Login_ibfk_1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Login_ibfk_2");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.ToTable("manufacturer");

                entity.HasIndex(e => e.CountryId, "CountryID");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.Property(e => e.ManufacturerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ManufacturerID");

                entity.Property(e => e.CountryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CountryID");

                entity.Property(e => e.ManufacturerAddr).HasMaxLength(65);

                entity.Property(e => e.ManufacturerEmail).HasMaxLength(65);

                entity.Property(e => e.ManufacturerName).HasMaxLength(45);

                entity.Property(e => e.ManufacturerPhone).HasColumnType("bigint(20)");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(12)
                    .IsFixedLength();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Manufacturers)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Manufacturer_ibfk_1");

                entity.HasOne(d => d.PostalCodeNavigation)
                    .WithMany(p => p.Manufacturers)
                    .HasForeignKey(d => d.PostalCode)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Manufacturer_ibfk_2");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.HasIndex(e => e.CustomerId, "CustomerID");

                entity.HasIndex(e => e.DeliveryId, "DeliveryID");

                entity.HasIndex(e => e.OrderItemId, "OrderItemID");

                entity.HasIndex(e => e.PaymentId, "PaymentID");

                entity.HasIndex(e => e.ProductId, "ProductID");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("OrderID");

                entity.Property(e => e.CustomerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CustomerID");

                entity.Property(e => e.DeliveryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("DeliveryID");

                entity.Property(e => e.OrderItemId)
                    .HasColumnType("int(11)")
                    .HasColumnName("OrderItemID");

                entity.Property(e => e.PaymentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("PaymentID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductID");

                entity.Property(e => e.TotalPrice).HasPrecision(10);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Order_ibfk_4");

                entity.HasOne(d => d.Delivery)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliveryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Order_ibfk_5");

                entity.HasOne(d => d.OrderItem)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderItemId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Order_ibfk_2");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Order_ibfk_3");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Order_ibfk_1");
            });

            modelBuilder.Entity<Orderitem>(entity =>
            {
                entity.ToTable("orderitem");

                entity.HasIndex(e => e.ProductId, "ProductID");

                entity.Property(e => e.OrderItemId)
                    .HasColumnType("int(11)")
                    .HasColumnName("OrderItemID");

                entity.Property(e => e.Price).HasPrecision(10);

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductID");

                entity.Property(e => e.Quantity)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orderitems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("OrderItem_ibfk_1");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment");

                entity.HasIndex(e => e.CustomerId, "CustomerID");

                entity.Property(e => e.PaymentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("PaymentID");

                entity.Property(e => e.Amount).HasPrecision(10);

                entity.Property(e => e.CustomerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CustomerID");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentMethod).HasMaxLength(45);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Payment_ibfk_1");
            });

            modelBuilder.Entity<Postalcode>(entity =>
            {
                entity.HasKey(e => e.PostalCode1)
                    .HasName("PRIMARY");

                entity.ToTable("postalcode");

                entity.HasIndex(e => e.CountryId, "CountryID");

                entity.Property(e => e.PostalCode1)
                    .HasMaxLength(12)
                    .HasColumnName("PostalCode")
                    .IsFixedLength();

                entity.Property(e => e.City).HasMaxLength(45);

                entity.Property(e => e.CountryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CountryID")
                    .HasDefaultValueSql("'61'");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Postalcodes)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("PostalCode_ibfk_1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.CategoryId, "CategoryID");

                entity.HasIndex(e => e.ManufacturerId, "ManufacturerID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductID");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CategoryID");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Dimensions).HasMaxLength(30);

                entity.Property(e => e.Image).HasColumnType("longblob")
                .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ManufacturerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ManufacturerID");

                entity.Property(e => e.ProductName).HasMaxLength(45);

                entity.Property(e => e.ProductPrice).HasPrecision(10);

                entity.Property(e => e.Weight).HasPrecision(5);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Product_ibfk_1");

                entity.HasOne(d => d.Manufacture)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Product_ibfk_2");
            });

            modelBuilder.Entity<ProductColor>(entity =>
            {
                entity.ToTable("product_color");

                entity.HasIndex(e => e.ColorId, "ColorID");

                entity.HasIndex(e => e.ProductId, "ProductID");

                entity.Property(e => e.ProductColorId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductColorID");

                entity.Property(e => e.ColorId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ColorID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductID");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.ProductColors)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Product_Color_ibfk_2");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductColors)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Product_Color_ibfk_1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(45);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("stock");

                entity.HasIndex(e => e.CountryId, "CountryID");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.Property(e => e.StockId)
                    .HasColumnType("int(11)")
                    .HasColumnName("StockID");

                entity.Property(e => e.CountryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CountryID");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(12)
                    .IsFixedLength();

                entity.Property(e => e.StockAddr).HasMaxLength(45);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Stock_ibfk_1");

                entity.HasOne(d => d.PostalCodeNavigation)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.PostalCode)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Stock_ibfk_2");
            });

            modelBuilder.Entity<StockProduct>(entity =>
            {
                entity.ToTable("stock_product");

                entity.HasIndex(e => e.ProductId, "ProductID");

                entity.HasIndex(e => e.StockId, "StockID");

                entity.Property(e => e.StockProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("StockProductID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasColumnType("int(11)");

                entity.Property(e => e.StockId)
                    .HasColumnType("int(11)")
                    .HasColumnName("StockID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.StockProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Stock_Product_ibfk_1");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.StockProducts)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Stock_Product_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
