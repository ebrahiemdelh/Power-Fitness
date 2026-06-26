namespace Power_Fitness.DAL.Context.Configurations
{
    public class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.Name)
               .HasMaxLength(50)
               .HasColumnType("nvarchar");
            builder.Property(p => p.Email)
                .HasMaxLength(100)
                .HasColumnType("nvarchar");

            builder.Property(p => p.Phone)
                .HasMaxLength(11)
                .HasColumnType("char");

            builder.Property(x => x.Gender).HasConversion<string>()
                .HasMaxLength(10)
                .HasColumnType("nvarchar");


            builder.OwnsOne(m => m.Address, a =>
            {
                a.Property(p => p.BuildingNo)
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar");
                a.Property(p => p.Street)
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
                a.Property(p => p.City)
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar");
            });

        }
    }
}
