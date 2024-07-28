using users_microservice.Domain.Entities;
using users_microservice.Domain.Repository;
using users_microservice.Repository.Data;
using Microsoft.EntityFrameworkCore;
using static users_microservice.Application.Dtos.ServiceResponses;


namespace users_microservice.Repository.Mysql
{
    public class AdminRepository : IAdminRepository
    {
        private readonly MySqlIdentityContext _context;

        public AdminRepository(MySqlIdentityContext context)
        {
            _context = context;
        }

        // Métodos para manejar transacciones
        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
        
        public async Task<GeneralResponse> CreateUser(UserModel user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "User created successfully", 200);
        }

        public async Task<AdminModel?> GetUserById(int id)
        {
            return await _context.Admins.FindAsync(id);
        }

        public async Task<AdminModel?> GetUserByEmail(string email)
        {
            return await _context.Admins.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<GeneralResponse> UpdateUser(UserModel userDto)
        {
            _context.Users.Update(userDto);
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "User updated successfully", 200);
        }

        public async Task<GeneralResponse> DeleteUser(UserModel user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "User deleted successfully", 200);
        }

        // Método para obtener todos los administradores
        public async Task<List<AdminModel>> GetAllAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        // Método para obtener todos los estudiantes
        public async Task<List<StudentModel>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // Nuevos métodos para manejar estudiantes
        public async Task<GeneralResponse> CreateStudent(StudentModel student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "Student created successfully", 200);
        }

        public async Task<StudentModel?> GetStudentById(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<GeneralResponse> UpdateStudent(StudentModel student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "Student updated successfully", 200);
        }

        public async Task<GeneralResponse> DeleteStudent(StudentModel student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "Student deleted successfully", 200);
        }

    }
}
