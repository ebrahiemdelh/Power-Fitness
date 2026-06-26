namespace Power_Fitness.DAL.Context.Configurations
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            //builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .HasColumnType("nvarchar");



            builder.Property(p => p.Description).HasMaxLength(200);

            //builder.Property(p => p.Price).HasColumnType("decimal(10,2)");
            builder.Property(p => p.Price).HasPrecision(10, 2);

            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.IsActive).HasDefaultValue(true);

            builder.ToTable(t => t.HasCheckConstraint("CK_Plan_DurationDays", "[DurationDays] BETWEEN 1 AND 365"));
        }
    }
}
