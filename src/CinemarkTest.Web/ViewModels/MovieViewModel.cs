using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CinemarkTest.Web.ViewModels;

public class MovieViewModel
{
    [Key]
    public Guid Id { get; set; }
    
    [DisplayName("Nome")]
    public string Name { get; set; }
    
    [DisplayName("Sinopse")]
    public string Synopsis { get; set; }
    
    [DisplayName("Classificação")]
    public int Rating { get; set; }
    
    [DisplayName("Duração")]
    public int Runtime { get; set; }
}