namespace Power_Fitness.DAL.Context.Configurations
{
    internal class SessionConfiguration:IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.Property(s => s.StartDate)
                .IsRequired()
                .HasColumnType("datetime");
            builder.Property(s => s.EndDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.ToTable(x=>
            {
                x.HasCheckConstraint("CK_Session_Dates", "[StartDate] < [EndDate]");
                x.HasCheckConstraint("CK_Session_Capacity", "[Capacity] Between 1 and 25");
            });
        }
    }
}
