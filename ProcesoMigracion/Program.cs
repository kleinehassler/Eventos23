// See https://aka.ms/newusing Clases;
using Newtonsoft.Json;

Console.WriteLine("Inicio de Migracion " + DateTime.Now);

// Ruta del archivo JSON
string rutaArchivo = @"D:\CESAR\2023\personal\curso virtuales\NET\Sesion 05\ProyectoFinalEventos\DATA\eventos.json";

// Leer el JSON como una cadena
string json = System.IO.File.ReadAllText(rutaArchivo);

// Deserializar el JSON en un objeto C#
Evento[] eventos = JsonConvert.DeserializeObject<Evento[]>(json);

//imprimir(eventos);



var mis_eventos = eventos.Where(p => p.categoria == "Tecnología");
//Console.WriteLine("Uso de Filtro de ventos de Tecnologia");
//imprimir(mis_eventos);


var primer_elemento = eventos.Where(p => p.categoria == "retretret").FirstOrDefault(new Evento() { idevento = 0, nombre = "Defecto" });
//Console.WriteLine("Uso de Primer Elemento");
//imprimirUno(primer_elemento);



var ultimo_elemento = eventos.Last();
//Console.WriteLine("Uso de Ultimo Elemento");
//imprimirUno(ultimo_elemento);


var mis_eventos_ordenados = eventos.OrderBy(p => p.presupuesto);
//Console.WriteLine("Uso de Orden");
//imprimir(mis_eventos_ordenados);



var mis_eventos_ordenados_des = eventos.OrderByDescending(parametro => parametro.presupuesto);
//Console.WriteLine("Uso de Orden DESC");
//imprimir(mis_eventos_ordenados_des);



var mis_eventos_select = eventos.Select(evento => new { titulo = evento.nombre, monto = evento.presupuesto }).ToList();
/*Console.WriteLine("Uso de Select");
foreach (var item in mis_eventos_select) {
    Console.WriteLine(item.ToString());
}*/

var contar = eventos.Where(p => p.categoria == "Tecnología").Count();
//Console.WriteLine("Cantidad de Eventos :" + contar);


var contarCondicion = eventos.LongCount(p => p.categoria == "Tecnología");
//Console.WriteLine("Cantidad de Eventos de Tecnologia :" + contarCondicion);



var sumarPresupuesto = eventos.Sum(p => p.presupuesto);
//Console.WriteLine("Total Presupuesto de todos los Eventos :" + sumarPresupuesto);


var hayEventoPresupuestoMeno10000 = eventos.Any(p => p.presupuesto < 12000);
//Console.WriteLine("Hay Evento con Presupuesto menor a 12000:" + hayEventoPresupuestoMeno10000);



var TodosEventoPresupuestoMayor0 = eventos.All(p => p.presupuesto > 0);
///Console.WriteLine("Todos los eventos tiene Presupuesto mayor a 0:" + TodosEventoPresupuestoMayor0);


var gruposPorCategoria = eventos.GroupBy(p => p.categoria);
/*foreach (var grupo in gruposPorCategoria)
{
    Console.WriteLine($"Eventos de la categoría '{grupo.Key}':");

    imprimir(grupo);

    Console.WriteLine();
}*/


var mis_eventos_tecnologia = eventos.Where(p => p.categoria == "Tecnología");
var mis_eventos_negocios = eventos.Where(p => p.categoria == "Negocios");
var nueva_lista = mis_eventos_negocios.Concat(mis_eventos_tecnologia);
//Console.WriteLine("Uso de Concat");
//imprimir(nueva_lista);



var categoriasDistintas = eventos.Select(evento => evento.categoria).Distinct().Order();
/*foreach (var evento in categoriasDistintas)
{
    Console.WriteLine($"Categoría: {evento}");
}
*/


var listaPaginada = eventos.Skip(10).Take(10);

//imprimir(listaPaginada);

//return;

float presupuestoMinimo = (float) eventos.Min(p => p.presupuesto);
//Console.WriteLine("Presupuesto Minimo:" + presupuestoMinimo);
//return;
float presupuestoMaximo = (float)eventos.Max(p => p.presupuesto);
//Console.WriteLine("Presupuesto Maximo:" + presupuestoMaximo);
//return;

float productoPresupuestos = eventos.Aggregate(0f, (acumulador, evento) => (float)(acumulador + 2*evento.presupuesto));
//Console.WriteLine($"Producto de Presupuestos: {productoPresupuestos}");

//return;

Evento eventoMenorPresupuesto = eventos.MinBy(p => p.presupuesto);
//Console.WriteLine("Eventos con menor presupuesto");
//imprimirUno(eventoMenorPresupuesto);
//return;
Evento eventoMayorPresupuesto = eventos.MaxBy(p => p.presupuesto);
//Console.WriteLine("Eventos con mayor presupuesto");
//imprimirUno(eventoMayorPresupuesto);

var listaPaginadaMayor = eventos.Where(p=>p.presupuesto>=10000 && p.presupuesto<=15000).OrderByDescending(p=>p.presupuesto).Skip(0).Take(5);

//imprimir(listaPaginadaMayor);

//return;
float presupuestoPromedio = (float)eventos.Average(p => p.presupuesto);
//Console.WriteLine("Presupuesto Promedio:" + presupuestoPromedio);

var librosAgrupadosDiccionario = eventos.ToLookup(p => p.nombre[0], p => p);
//imprimirDiccionario(librosAgrupadosDiccionario, 'F');


List<Evento> listaEventos = new List<Evento>();
listaEventos.Add(new Evento() { idevento=1, nombre = "Curso de .NET", idtipo_evento =1 });
listaEventos.Add(new Evento() { idevento = 2, nombre = "Curso de Angular", idtipo_evento = 2 });
listaEventos.Add(new Evento() { idevento = 3, nombre = "Curso de Blazor", idtipo_evento = 2 });
List<TipoEvento> listaTipos = new List<TipoEvento>();
listaTipos.Add(new TipoEvento() { idtipo_evento=1, nombre = "Tecnologia" });
listaTipos.Add(new TipoEvento() { idtipo_evento = 2, nombre = "Programacion" });

var listaJoin = listaEventos
    .Join(listaTipos,
          evento => evento.idtipo_evento,
          tipo => tipo.idtipo_evento,
          (evento, tipo) => new { evento.idevento, nombreEvento = evento.nombre, tipo.idtipo_evento, tipoEvento = tipo.nombre });

foreach (var item in listaJoin)
{
    Console.WriteLine($"idevento = {item.idevento}, Nombre= {item.nombreEvento}, idtipo_evento= {item.idtipo_evento}, tipoEvento= {item.tipoEvento}");
}


return;

void imprimir(IEnumerable<Evento> eventos)
{

    foreach (var evento in eventos)
    {
        Console.WriteLine($"ID: {evento.idevento}, Nombre: {evento.nombre}, Fecha: {evento.fecha}, Presupuesto: {evento.presupuesto:C}, Categoría: {evento.categoria}");
    }
}
void imprimirUno(Evento evento)
{

    Console.WriteLine($"ID: {evento.idevento}, Nombre: {evento.nombre}, Fecha: {evento.fecha}, Presupuesto: {evento.presupuesto:C}, Categoría: {evento.categoria}");

}
void imprimirSelect(List<(string titulo, float monto)> eventos)
{

    foreach (var evento in eventos)
    {
        Console.WriteLine($"titulo: {evento.titulo}, monto: {evento.monto}");
    }
}

void imprimirDiccionario(ILookup<char, Evento> eventos, char letra)
{

    foreach (var evento in eventos[letra])
    {
        imprimirUno(evento);
    }
}

class Evento { 
    public int? idevento {  get; set; }
    public string? nombre { get; set; }
    public DateTime? fecha { get; set; }
    public float? presupuesto { get; set; }
    public string? categoria { get; set; }
    public int idtipo_evento { get; set; }
}

class TipoEvento { 
    public int idtipo_evento { get; set; }
    public string nombre { get; set; }
            
}