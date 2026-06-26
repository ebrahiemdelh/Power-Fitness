namespace Power_Fitness.DAL.Context.Configurations
{
    internal class CategoryConfiguration:IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar");

            builder.HasData(
                new Category { Id = 1, Name = "Cardio" },
                new Category { Id = 2, Name = "Strength" },
                new Category { Id = 3, Name = "Flexibility" },
                new Category { Id = 4, Name = "CrossFit" }
            );
        }
    }
}
