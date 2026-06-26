namespace Power_Fitness.DAL.Context.Configurations
{
    public class MemberConfiguration : GymUserConfiguration<Member>, IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.Photo)
                .IsRequired()
                .HasMaxLength(255);



            builder.ToTable(x =>
            {
                x.HasCheckConstraint("EmailCheck", "Email LIKE '%@%.%'");
                x.HasCheckConstraint("PhoneCheck", "Phone LIKE '01%' AND LEN(Phone) = 11");
            });
        }
    }
}
