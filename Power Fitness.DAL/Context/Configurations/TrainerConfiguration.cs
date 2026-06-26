namespace Power_Fitness.DAL.Context.Configurations
{
    internal class TrainerConfiguration : GymUserConfiguration<Trainer>, IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            base.Configure(builder);
            builder.Property(x=>x.Specialty).HasConversion<string>()
                .HasMaxLength(50)
                .HasColumnType("nvarchar");

            builder.OwnsOne(t => t.Address, a =>
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
            builder.ToTable(x =>
            {
                x.HasCheckConstraint("EmailCheck", "Email LIKE '%@%.%'");
                x.HasCheckConstraint("PhoneCheck", "Phone LIKE '01%' AND LEN(Phone) = 11");
            });
        }
    }
}
