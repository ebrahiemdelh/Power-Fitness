namespace Power_Fitness.DAL.Context.Configurations
{
    internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.Property(hr => hr.Weight)
                .IsRequired()
                .HasColumnType("decimal(5,2)");
            builder.Property(hr => hr.Height)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(hr => hr.BloodType)
                .IsRequired()
                .HasMaxLength(5)
                .HasColumnType("char");
            builder.Property(hr => hr.Note).
                HasMaxLength(200);
        }
    }
}
