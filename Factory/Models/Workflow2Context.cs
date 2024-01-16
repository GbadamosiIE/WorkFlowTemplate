using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Factory.Models;

public partial class Workflow2Context : DbContext
{
    public Workflow2Context()
    {
    }

    public Workflow2Context(DbContextOptions<Workflow2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Flow> Flows { get; set; }

    public virtual DbSet<FlowCallback> FlowCallbacks { get; set; }

    public virtual DbSet<FlowInstance> FlowInstances { get; set; }

    public virtual DbSet<FlowInstanceStatus> FlowInstanceStatuses { get; set; }

    public virtual DbSet<Workflow> Workflows { get; set; }

    public virtual DbSet<WorkflowInstance> WorkflowInstances { get; set; }

    public virtual DbSet<WorkflowInstanceStatus> WorkflowInstanceStatuses { get; set; }

    public virtual DbSet<WorkflowVariable> WorkflowVariables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.ToTable("Branch");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(250);

            entity.HasOne(d => d.Flow).WithMany(p => p.BranchFlows)
                .HasForeignKey(d => d.FlowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Branch_FlowId");

            entity.HasOne(d => d.NextFlow).WithMany(p => p.BranchNextFlows)
                .HasForeignKey(d => d.NextFlowId)
                .HasConstraintName("FK_Branch_NextFlowId");
        });

        modelBuilder.Entity<Flow>(entity =>
        {
            entity.ToTable("Flow");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(250);

            entity.HasOne(d => d.Workflow).WithMany(p => p.Flows)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flow_WorkflowId");
        });

        modelBuilder.Entity<FlowCallback>(entity =>
        {
            entity.ToTable("FlowCallback");

            entity.Property(e => e.CallbackUrl).HasMaxLength(2048);

            entity.HasOne(d => d.Flow).WithMany(p => p.FlowCallbacks)
                .HasForeignKey(d => d.FlowId)
                .HasConstraintName("FK_FlowCallback_FlowId");
        });

        modelBuilder.Entity<FlowInstance>(entity =>
        {
            entity.ToTable("FlowInstance");

            entity.Property(e => e.EndedOn).HasColumnType("datetime");
            entity.Property(e => e.StartedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Flow).WithMany(p => p.FlowInstances)
                .HasForeignKey(d => d.FlowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FlowInstance_FlowId");

            entity.HasOne(d => d.FlowInstanceStatus).WithMany(p => p.FlowInstances)
                .HasForeignKey(d => d.FlowInstanceStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FlowInstance_FlowInstanceStatusId");

            entity.HasOne(d => d.WorkflowInstance).WithMany(p => p.FlowInstances)
                .HasForeignKey(d => d.WorkflowInstanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FlowInstance_WorkflowInstanceId");
        });

        modelBuilder.Entity<FlowInstanceStatus>(entity =>
        {
            entity.ToTable("FlowInstanceStatus");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Workflow>(entity =>
        {
            entity.ToTable("Workflow");

            entity.Property(e => e.ApplicationName).HasMaxLength(512);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1024);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<WorkflowInstance>(entity =>
        {
            entity.ToTable("WorkflowInstance");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndedOn).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(1024);
            entity.Property(e => e.StartedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Workflow).WithMany(p => p.WorkflowInstances)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowInstance_WorkflowId");

            entity.HasOne(d => d.WorkflowInstanceStatus).WithMany(p => p.WorkflowInstances)
                .HasForeignKey(d => d.WorkflowInstanceStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowInstance_WorkflowInstanceStatusId");
        });

        modelBuilder.Entity<WorkflowInstanceStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_WorkflowRunStatus");

            entity.ToTable("WorkflowInstanceStatus");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WorkflowVariable>(entity =>
        {
            entity.ToTable("WorkflowVariable");

            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Workflow).WithMany(p => p.WorkflowVariables)
                .HasForeignKey(d => d.WorkflowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkflowVariable_WorkflowId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
