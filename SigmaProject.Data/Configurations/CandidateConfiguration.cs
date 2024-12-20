namespace SigmaProject.Data.Configurations;

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable(nameof(Candidate).Humanize().Pascalize());

        builder.HasKey(x => x.Id);
        builder.Property(e => e.Id);

        builder.Property(e => e.FirstName).IsRequired(true).HasMaxLength(100);
        builder.Property(e => e.LastName).IsRequired(true).HasMaxLength(100);


        builder.HasIndex(e => e.Email).IsUnique();
        builder.Property(e => e.Email).IsRequired(true).HasMaxLength(255);

        builder.Property(e => e.CallTimeInterval).HasMaxLength(50);
        builder.Property(e => e.GitHubProfileUrl).HasMaxLength(500);
        builder.Property(e => e.LinkedInProfileUrl).HasMaxLength(500);
        builder.Property(e => e.PhoneNumber).HasMaxLength(17);
        builder.Property(e => e.Comment).IsRequired(true).HasMaxLength(1000);
    }
}