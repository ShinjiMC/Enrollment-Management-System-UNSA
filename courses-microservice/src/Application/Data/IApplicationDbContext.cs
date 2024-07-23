using Domain.Customers;
using Domain.Courses;

using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }
    DbSet<Course> Courses { get; set; }
    DbSet<CoursePrerequisite> CoursePrerequisites { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}