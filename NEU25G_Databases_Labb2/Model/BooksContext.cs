using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace NEU25G_Databases_Labb2;

public partial class BooksContext : DbContext
{
    public BooksContext()
    {
    }

    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeSalesAndOrder> EmployeeSalesAndOrders { get; set; }

    public virtual DbSet<Format> Formats { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<MostSalesGenre> MostSalesGenres { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleItem> SaleItems { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoreBook> StoreBooks { get; set; }

    public virtual DbSet<TitlarPerFörfattare> TitlarPerFörfattares { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;

        if (string.IsNullOrWhiteSpace(connectionString)) throw new InvalidOperationException("Connection string missing.");

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC07937490EA");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Isbn13).HasName("PK__Books__3BF79E036EF109B6");

            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("ISBN13");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Format).WithMany(p => p.Books)
                .HasForeignKey(d => d.FormatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__FormatId__2A8B4280");

            entity.HasOne(d => d.Genre).WithMany(p => p.Books)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__GenreId__29971E47");

            entity.HasMany(d => d.Authors).WithMany(p => p.Isbn13s)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookAutho__Autho__2E5BD364"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("Isbn13")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookAutho__ISBN1__2D67AF2B"),
                    j =>
                    {
                        j.HasKey("Isbn13", "AuthorId").HasName("PK__BookAuth__6CFA31C08BE5AF36");
                        j.ToTable("BookAuthors");
                        j.IndexerProperty<string>("Isbn13")
                            .HasMaxLength(13)
                            .IsUnicode(false)
                            .HasColumnName("ISBN13");
                    });
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F113978C08E");

            entity.HasOne(d => d.Store).WithMany(p => p.Employees)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employees__Store__33208881");
        });

        modelBuilder.Entity<EmployeeSalesAndOrder>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("EmployeeSalesAndOrders");

            entity.Property(e => e.AnställdsId).HasColumnName("Anställds ID");
            entity.Property(e => e.AntalBeställdaOrdrar).HasColumnName("Antal beställda ordrar");
            entity.Property(e => e.AntalFörsäljningar).HasColumnName("Antal försäljningar");
            entity.Property(e => e.AntalMottagnaOrdrar).HasColumnName("Antal mottagna ordrar");
            entity.Property(e => e.AntalSkickadeOrdrar).HasColumnName("Antal skickade ordrar");
            entity.Property(e => e.AntalSåldaArtiklar).HasColumnName("Antal sålda artiklar");
            entity.Property(e => e.FörsäljningsbeloppIKr)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Försäljningsbelopp i kr");
        });

        modelBuilder.Entity<Format>(entity =>
        {
            entity.HasKey(e => e.FormatId).HasName("PK__Formats__5D3DCB598D707C71");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385057E51A1DFF5");
        });

        modelBuilder.Entity<MostSalesGenre>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MostSalesGenres");

            entity.Property(e => e.OfBooks).HasColumnName("# of books");
            entity.Property(e => e.OfSales).HasColumnName("# of sales");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF07E4CB3E");

            entity.Property(e => e.OrderDate).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.OrderType).HasDefaultValue("FrånUtgivare");
            entity.Property(e => e.OrderingEmployeeId).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.ReceivedDate).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.ReceivedEmployeeId).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.SenderEmployeeId).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.SenderStoreId).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.ShippedDate).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.StatusId).HasDefaultValue(1);

            entity.HasOne(d => d.DestinationStore).WithMany(p => p.OrderDestinationStores)
                .HasForeignKey(d => d.DestinationStoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Destinat__4EC8A2F6");

            entity.HasOne(d => d.OrderingEmployee).WithMany(p => p.OrderOrderingEmployees)
                .HasForeignKey(d => d.OrderingEmployeeId)
                .HasConstraintName("FK__Orders__Ordering__50B0EB68");

            entity.HasOne(d => d.ReceivedEmployee).WithMany(p => p.OrderReceivedEmployees)
                .HasForeignKey(d => d.ReceivedEmployeeId)
                .HasConstraintName("FK__Orders__Received__529933DA");

            entity.HasOne(d => d.SenderEmployee).WithMany(p => p.OrderSenderEmployees)
                .HasForeignKey(d => d.SenderEmployeeId)
                .HasConstraintName("FK__Orders__SenderEm__51A50FA1");

            entity.HasOne(d => d.SenderStore).WithMany(p => p.OrderSenderStores)
                .HasForeignKey(d => d.SenderStoreId)
                .HasConstraintName("FK__Orders__SenderSt__4FBCC72F");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Orders__StatusId__538D5813");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.Isbn13 }).HasName("PK__OrderDet__E02F222F1C02DBA3");

            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("ISBN13");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Isbn13Navigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.Isbn13)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__ISBN1__575DE8F7");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__5669C4BE");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__OrderSta__C8EE2063F3CEE3BC");

            entity.ToTable("OrderStatus");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sales__1EE3C3FF69610BDB");

            entity.Property(e => e.SaleDateTime).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Employee).WithMany(p => p.Sales)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sales__EmployeeI__3CA9F2BB");

            entity.HasOne(d => d.Store).WithMany(p => p.Sales)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sales__StoreId__3BB5CE82");
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.HasKey(e => new { e.SaleId, e.Isbn13 }).HasName("PK__SaleItem__3D5CBA1F35441488");

            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("ISBN13");
            entity.Property(e => e.Quantity).HasDefaultValue(1);
            entity.Property(e => e.SalePrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Isbn13Navigation).WithMany(p => p.SaleItems)
                .HasForeignKey(d => d.Isbn13)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaleItems__ISBN1__416EA7D8");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleItems)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaleItems__SaleI__407A839F");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Stores__3B82F10104E27D30");
        });

        modelBuilder.Entity<StoreBook>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.Isbn13 }).HasName("PK__StoreBoo__183D88E1A4882FCD");

            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("ISBN13");

            entity.HasOne(d => d.Isbn13Navigation).WithMany(p => p.StoreBooks)
                .HasForeignKey(d => d.Isbn13)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StoreBook__ISBN1__37E53D9E");

            entity.HasOne(d => d.Store).WithMany(p => p.StoreBooks)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StoreBook__Store__36F11965");
        });

        modelBuilder.Entity<TitlarPerFörfattare>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TitlarPerFörfattare");

            entity.Property(e => e.Age)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Lagervärde)
                .HasMaxLength(44)
                .IsUnicode(false);
            entity.Property(e => e.Titlar)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
