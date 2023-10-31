// using System.Collections.Generic;
// using AcaoSolidariaApi.Data;
// using AcaoSolidariaApi.Models;

// namespace AcaoSolidariaApi.Services
// {
//     public class VoluntarioService : IVoluntarioService
//     {

//         private readonly List<Voluntario> voluntarios = new List<Voluntario>();

//         private readonly DataContext _context;

//         public VoluntarioService(DataContext context)
//         {
//             _context = context;
//         }


//         public void CriarVoluntario(Voluntario voluntario)
//         {

//             _context.Voluntarios.Add(voluntario);
//             _context.SaveChanges();
//         }


//         public void AtualizarVoluntario(Voluntario voluntario)
//         {
//             var voluntarioExistente = _context.Voluntarios.FirstOrDefault(v => v.Id == voluntario.Id);
//             if (voluntarioExistente != null)
//             {
//                 voluntarioExistente.Nome = voluntario.Nome; // Substitua 'Nome' pelo nome correto do campo que você deseja atualizar
//                 voluntarioExistente.Email = voluntario.Email; // Substitua 'Email' pelo nome correto do campo que você deseja atualizar
//                 voluntarioExistente.Senha = voluntario.Senha; // Substitua 'Senha' pelo nome correto do campo que você deseja atualizar

//                 _context.Voluntarios.Update(voluntarioExistente);
//                 _context.SaveChanges();
//             }
//         }




//         // public Voluntario ObterVoluntarioPorId(int id)
//         // {
//         //     var voluntario = voluntarios.Find(voluntario => voluntario.Id == id);
//         //     if (voluntario == null)
//         //     {
//         //         throw new Exception($"Voluntário com ID {id} não encontrado.");
//         //     }
//         //     return voluntario;
//         // }


//         public Voluntario ObterVoluntarioPorId(int id)
//         {
//             var voluntario = _context.Voluntarios.FirstOrDefault(v => v.Id == id);
//             if (voluntario == null)
//             {
//                 throw new Exception($"Voluntário com o ID {id} não encontrado.");
//             }
//             return voluntario;
//         }

//         public void DeletarVoluntario(int id)
//         {
//             var voluntario = _context.Voluntarios.FirstOrDefault(v => v.Id == id);
//             if (voluntario != null)
//             {
//                 _context.Voluntarios.Remove(voluntario);
//             }
//         }
//     }
// }
