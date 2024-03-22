﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetBankingApp.Infrastucture.Persistence.Contexts;

#nullable disable

namespace NetBankingApp.Infrastucture.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NetBankingApp.Core.Domain.Models.Beneficiary", b =>
                {
                    b.Property<string>("IdUser")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IdBeneficiary")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BeneficiaryAccountGuid")
                        .HasColumnType("int");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUser", "IdBeneficiary");

                    b.ToTable("Beneficiaries", (string)null);
                });

            modelBuilder.Entity("NetBankingApp.Core.Domain.Models.CreditCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<double>("Debt")
                        .HasColumnType("float");

                    b.Property<int>("Guid")
                        .HasColumnType("int");

                    b.Property<string>("IdCustomer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("LimitAmount")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("CreditCards", (string)null);
                });

            modelBuilder.Entity("NetBankingApp.Core.Domain.Models.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<double>("Debt")
                        .HasColumnType("float");

                    b.Property<int>("Guid")
                        .HasColumnType("int");

                    b.Property<string>("IdCustomer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("LoanAmount")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Loans", (string)null);
                });

            modelBuilder.Entity("NetBankingApp.Core.Domain.Models.PaymentLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("GuidAccountOrigin")
                        .HasColumnType("int");

                    b.Property<int>("GuidProductDestination")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PaymentLogs", (string)null);
                });

            modelBuilder.Entity("NetBankingApp.Core.Domain.Models.SavingAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("Guid")
                        .HasColumnType("int");

                    b.Property<string>("IdCustomer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<double>("Savings")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("SavingAccounts", (string)null);
                });

            modelBuilder.Entity("NetBankingApp.Core.Domain.Models.TransactionLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("GuidAccountDestination")
                        .HasColumnType("int");

                    b.Property<int>("GuidAccountOrigin")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TransactionLogs", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
