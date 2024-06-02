using Microsoft.EntityFrameworkCore;
using SimpleMinimalAPI.Data;

namespace SimpleMinimalAPI.Students
{
    public static class StudentsRoutes
    {
        public static void AddRoutesStudents(this WebApplication app)
        {
            var studentsRoutes = app.MapGroup("students");

            studentsRoutes.MapPost("", 
                async (AddStudentRequest request, AppDbContext context, CancellationToken ct) =>
            {
                bool studentExist = await context.Students.AnyAsync(x=> x.Nome == request.Nome, ct);

                if (studentExist)
                    return Results.Conflict("Already exists!");

                var newStudent = new Student(request.Nome);
                await context.Students.AddAsync(newStudent, ct);
                await context.SaveChangesAsync(ct);

                var studentReturn = new StudentDTO(newStudent.Id, newStudent.Nome);

                return Results.Ok(studentReturn);
            });

            studentsRoutes.MapGet("", async (AppDbContext context, CancellationToken ct) =>
            {
                var students = await context
                    .Students
                    .Where(x => x.Ativo == true)
                    .Select(k => new StudentDTO(k.Id, k.Nome))
                    .ToListAsync(ct);
                return students;
            });

            studentsRoutes.MapPut("{id}", async (Guid id,UpdateEstudanteRequest request, AppDbContext Context, CancellationToken ct) =>
            {
                var student = await Context.Students.SingleOrDefaultAsync(x => x.Id == id);

                if (student == null)
                    return Results.NotFound();

                student.AtualizarNome(request.Nome);
                await Context.SaveChangesAsync(ct);

                return Results.Ok(new StudentDTO(student.Id, student.Nome));
            });

            //soft delete
            studentsRoutes.MapDelete("{id}",async(Guid id, AppDbContext context, CancellationToken ct) =>
            {
                var student = await context.Students.SingleOrDefaultAsync(x => x.Id == id, ct);

                if(student == null)
                    Results.NotFound();

                student.Desativar();

                await context.SaveChangesAsync(ct);
            });
        }
    }
}
