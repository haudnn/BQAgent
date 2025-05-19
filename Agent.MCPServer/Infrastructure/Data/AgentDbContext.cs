using Agent.MCPServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agent.MCPServer.Infrastructure.Data;

public class AgentDbContext : DbContext
{

  public DbSet<User> Users { get; set; } = default!;
  // public DbSet<MessageAttachments> MessageAttachments { get; set; } = default!;
  public DbSet<Message> Messages { get; set; } = default!;
  public DbSet<Conversation> Conversations { get; set; } = default!;
  public DbSet<ConversationMember> ConversationMembers { get; set; } = default!;

  public AgentDbContext() { }
  public AgentDbContext(DbContextOptions<AgentDbContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // Configure User entity
    modelBuilder.Entity<User>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.Property(e => e.EmployeeCode).IsRequired();
      entity.Property(e => e.DisplayName).IsRequired();
      entity.Property(e => e.Email).IsRequired();
      entity.Property(e => e.PasswordHash).IsRequired();
      entity.HasIndex(e => e.EmployeeCode).IsUnique();
    });

    // Configure Conversation entity
    modelBuilder.Entity<Conversation>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Name).IsRequired();

      // Define relationship with User (CreatedBy)
      entity.HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
    });

    // Configure Message entity
    modelBuilder.Entity<Message>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Content).IsRequired();

      // Define relationship with Conversation
      entity.HasOne(m => m.Conversation)
            .WithMany()
            .HasForeignKey(m => m.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);

      // Define relationship with User (Sender)
      entity.HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
    });

    // Configure ConversationMember entity
    modelBuilder.Entity<ConversationMember>(entity =>
    {
      entity.HasKey(e => e.Id);

      // Define relationship with Conversation
      entity.HasOne(cm => cm.Conversation)
            .WithMany()
            .HasForeignKey(cm => cm.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);

      // Define relationship with User
      entity.HasOne(cm => cm.User)
            .WithMany()
            .HasForeignKey(cm => cm.UserId)
            .OnDelete(DeleteBehavior.Cascade);

      // Create unique index on ConversationId and UserId
      entity.HasIndex(cm => new { cm.ConversationId, cm.UserId }).IsUnique();
    });
  }
}