using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq.Expressions;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Utilities.Services.Persistence.EF
{
    public static class DbModelBuilderExtensions
    {
        public static PrimitivePropertyConfiguration Index<TWithRelationalId>(
            this DbModelBuilder modelBuilder, Expression<Func<TWithRelationalId, string>> propertyGetter,
            bool? isUnique = false, string name = null, int? order = null)
            where TWithRelationalId : class, IWithRelationalId
        {
            return modelBuilder.Entity<TWithRelationalId>()
                .Property(propertyGetter)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    CreateIndexAnnotation(isUnique, name, order));
        }

        public static PrimitivePropertyConfiguration Index<TWithRelationalId, TProperty>(
            this DbModelBuilder modelBuilder, Expression<Func<TWithRelationalId, TProperty?>> propertyGetter,
            bool? isUnique = false, string name = null, int? order = null)
            where TWithRelationalId : class, IWithRelationalId
            where TProperty : struct
        {
            return modelBuilder.Entity<TWithRelationalId>()
                .Property(propertyGetter)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    CreateIndexAnnotation(isUnique, name, order));
        }

        public static PrimitivePropertyConfiguration Index<TWithRelationalId, TProperty>(
            this DbModelBuilder modelBuilder, Expression<Func<TWithRelationalId, TProperty>> propertyGetter,
            bool? isUnique = false, string name = null, int? order = null)
            where TWithRelationalId : class, IWithRelationalId
            where TProperty : struct
        {
            return modelBuilder.Entity<TWithRelationalId>()
                .Property(propertyGetter)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    CreateIndexAnnotation(isUnique, name, order));
        }

        private static IndexAnnotation CreateIndexAnnotation(bool? isUnique = false, string name = null,
            int? order = null)
        {
            IndexAttribute indexAttribute;
            if (!string.IsNullOrWhiteSpace(name) && order.HasValue)
                indexAttribute = new IndexAttribute(name, order.Value);
            else if (!string.IsNullOrWhiteSpace(name))
                indexAttribute = new IndexAttribute(name);
            else
                indexAttribute = new IndexAttribute();

            if (isUnique.HasValue && isUnique.Value)
                indexAttribute.IsUnique = true;

            var indexAnnotation = new IndexAnnotation(indexAttribute);
            return indexAnnotation;
        }

        public static PrimitivePropertyConfiguration UniquelyIndex<TWithRelationalId, TProperty>(
            this DbModelBuilder modelBuilder, Expression<Func<TWithRelationalId, TProperty>> propertyGetter)
            where TWithRelationalId : class, IWithRelationalId
            where TProperty : struct
        {
            return Index(modelBuilder, propertyGetter, true);
        }

        public static PrimitivePropertyConfiguration UniquelyIndex<TWithRelationalId>(
            this DbModelBuilder modelBuilder, Expression<Func<TWithRelationalId, string>> propertyGetter)
            where TWithRelationalId : class, IWithRelationalId
        {
            return modelBuilder.Entity<TWithRelationalId>()
                .Property(propertyGetter)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute {IsUnique = true}));
        }

        public static PrimitivePropertyConfiguration UniquelyIndexEntityKey<TEntity>(this DbModelBuilder modelBuilder)
            where TEntity : class, IEntity
        {
            return modelBuilder.Entity<TEntity>()
                .Property(t => t.Key)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute {IsUnique = true}));
        }
    }
}