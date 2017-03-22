namespace tp10
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class archeoContext : DbContext
	{
		public archeoContext()
			: base("name=archeoContext")
		{
		}

		public virtual DbSet<Commune> Communes { get; set; }
		public virtual DbSet<departement> departements { get; set; }
		public virtual DbSet<intervention> interventions { get; set; }
		public virtual DbSet<periode> periodes { get; set; }
		public virtual DbSet<site_intervention> site_intervention { get; set; }
		public virtual DbSet<theme> themes { get; set; }
		public virtual DbSet<type_intervention> type_intervention { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Commune>()
				.Property(e => e.nom)
				.IsUnicode(false);

			modelBuilder.Entity<Commune>()
				.HasMany(e => e.site_intervention)
				.WithRequired(e => e.Commune)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<departement>()
				.Property(e => e.nom)
				.IsUnicode(false);

			modelBuilder.Entity<intervention>()
				.Property(e => e.ID_site)
				.IsUnicode(false);

			modelBuilder.Entity<periode>()
				.Property(e => e.nom)
				.IsUnicode(false);

			modelBuilder.Entity<periode>()
				.HasMany(e => e.site_intervention)
				.WithMany(e => e.periodes)
				.Map(m => m.ToTable("periodeIntervention").MapLeftKey("ID_periode").MapRightKey("ID_site"));

			modelBuilder.Entity<site_intervention>()
				.Property(e => e.ID_site)
				.IsUnicode(false);

			modelBuilder.Entity<site_intervention>()
				.Property(e => e.nom_site)
				.IsUnicode(false);

			modelBuilder.Entity<site_intervention>()
				.HasMany(e => e.interventions)
				.WithRequired(e => e.site_intervention)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<site_intervention>()
				.HasMany(e => e.themes)
				.WithMany(e => e.site_intervention)
				.Map(m => m.ToTable("themeIntervention").MapLeftKey("ID_site").MapRightKey("ID_theme"));

			modelBuilder.Entity<site_intervention>()
				.HasMany(e => e.type_intervention)
				.WithMany(e => e.site_intervention)
				.Map(m => m.ToTable("typeIntervention").MapLeftKey("ID_site").MapRightKey("ID_type"));

			modelBuilder.Entity<theme>()
				.Property(e => e.nom)
				.IsUnicode(false);

			modelBuilder.Entity<type_intervention>()
				.Property(e => e.nom)
				.IsUnicode(false);
		}
	}
}
