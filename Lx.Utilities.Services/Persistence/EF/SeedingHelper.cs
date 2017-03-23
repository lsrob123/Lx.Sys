namespace Lx.Utilities.Services.Persistence.EF {
    public static class SeedingHelper {
        //public static void AddOrUpdate<T>(T dataObject, Expression<Func<T, bool>> searchExpression, DbContext context)
        //    where T : class, IWithRelationalId
        //{
        //    var dbSet = context.Set<T>();
        //    var existing = dbSet.FirstOrDefault(searchExpression);
        //    if (existing == null)
        //    {
        //        dbSet.Add(dataObject);
        //        context.SaveChanges();
        //        return;
        //    }

        //    dataObject.SetId(existing.Id);
        //    context.Entry(existing).State = EntityState.Detached;
        //    dbSet.Attach(dataObject);
        //    context.Entry(dataObject).State = EntityState.Modified;
        //    context.SaveChanges();
        //}

        //public static void AddAnyway<T>(T dataObject, Expression<Func<T, bool>> searchExpression, DbContext context)
        //    where T : class, IWithRelationalId
        //{
        //    var dbSet = context.Set<T>();
        //    var existing = dbSet.FirstOrDefault(searchExpression);
        //    if (existing != null)
        //    {
        //        dbSet.Remove(existing);
        //    }

        //    dbSet.Add(dataObject);
        //    context.SaveChanges();
        //}

        //public static void AddOrUpdate<T>(T dataObject, Expression<Func<T, bool>> searchExpression, DbContext context)
        //where T : class, IWithRelationalId
        //{
        //    var dbSet = context.Set<T>();
        //    var existing = dbSet.FirstOrDefault(searchExpression);
        //    if (existing == null)
        //    {
        //        dbSet.Add(dataObject);
        //        context.SaveChanges();
        //        return;
        //    }

        //    dataObject.AssignPropertyValuesTo(existing, "Id", "Key");
        //    context.Entry(existing).State = EntityState.Modified;
        //    context.SaveChanges();
        //}
    }
}