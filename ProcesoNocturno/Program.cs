using Entidades;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Utilidades;



IAnimal perro = null;
IAnimal gato = null;

perro = new Perro();

perro.mostraSonido();

gato = new Gato();

gato.mostraSonido();



Evento cursoPowerAutomate = new Evento(3, "Curso de Power Automate", new DateTime(2023, 11, 25));

string resultado = cursoPowerAutomate.mostrarDatosEvento();

Console.WriteLine(resultado);

cursoPowerAutomate.mostrarDiasParaEvento();



const int DIAS_A_NOTIFICAR = 1;

Console.WriteLine("Fecha y Hora de Inicio del Proceso : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

Evento eventoNet = new Evento() { Idevento=1, Nombre = "Programacion en .NET", Fecha = new DateTime(2023,11,10), Estado = EstadoEvento.Pendiente};
Evento eventoAngular = new Evento() { Idevento = 2, Nombre = "Programacion en ANGULAR", Fecha = new DateTime(2023, 11, 11), Estado = EstadoEvento.Pendiente };

List<Evento> listaEVentos = new List<Evento>();
listaEVentos.Add(eventoNet);
listaEVentos.Add(eventoAngular);

Invitado invitado1 = new Invitado() { Nombres="Cesar", Apellidos= "Nunton", Telefono = "+51961746805", Correo = "nuntoncesar@hotmailes", Sexo = Sexo.Hombre, TipoInvitado = TipoInvitado.Adulto, Estado=EstadoInvitado.Registrado, Idevento=1 };
Invitado invitado2 = new Invitado() { Nombres = "Juan", Apellidos = "Perez", Telefono = "+51941970813", Correo = "nuntoncesar@hotmailcom", Sexo = Sexo.Hombre, TipoInvitado = TipoInvitado.Adulto, Estado = EstadoInvitado.Registrado, Idevento = 2 };
Invitado invitado3 = new Invitado() { Nombres = "Valetin", Apellidos = "Guardamino", Telefono = "+51997175343", Correo = "vguardamino@hotmailcom", Sexo = Sexo.Hombre, TipoInvitado = TipoInvitado.Adulto, Estado = EstadoInvitado.Registrado, Idevento = 1 };
Invitado invitado4 = new Invitado() { Nombres = "Lincol", Apellidos = "", Telefono = "+51944941443", Correo = "LGDOLORESSGMAIL.COM", Sexo = Sexo.Hombre, TipoInvitado = TipoInvitado.Adulto, Estado = EstadoInvitado.Registrado, Idevento = 1 };

Invitado[] listaInvitados = new Invitado[4];
listaInvitados[0] = invitado1;
listaInvitados[1] = invitado2;
listaInvitados[2] = invitado3;
listaInvitados[3] = invitado4;

foreach (var itemEvento in listaEVentos)
{
    Console.WriteLine($"El evento : {itemEvento.Nombre}");

    var diasTime = itemEvento.Fecha - DateTime.Now.Date;
    var dias = diasTime.Days;
    Console.WriteLine($"Dias para el evento {dias}");
    if (DIAS_A_NOTIFICAR == dias) {
        //Ingresar a notificar al invitado.
        var lista_invitados_filtrada = listaInvitados.Where(p=>p.Idevento == itemEvento.Idevento).ToList();
        foreach (var itemInvitado in lista_invitados_filtrada) {
            Console.WriteLine($"Nombre del invitado : {itemInvitado.Nombres} {itemInvitado.Apellidos}");
            string mensaje = "";
            switch (itemInvitado.Sexo)
            {
                case Sexo.Hombre:
                    mensaje = "Estimado, " + itemInvitado.Nombres + " " + itemInvitado.Apellidos + ", su evento " + itemEvento.Nombre + " se iniciara mañana";
                    break;
                case Sexo.Mujer:
                    mensaje = "Estimada, " + itemInvitado.Nombres + " " + itemInvitado.Apellidos + ", su evento " + itemEvento.Nombre + " se iniciara mañana";
                    break;
                case Sexo.Otro:
                    mensaje = "Estimad@, " + itemInvitado.Nombres + " " + itemInvitado.Apellidos + ", su evento " + itemEvento.Nombre + " se iniciara mañana";
                    break;
                default:
                    mensaje = "Señor, " + itemInvitado.Nombres + " " + itemInvitado.Apellidos + ", su evento " + itemEvento.Nombre + " se iniciara mañana";
                    break;
            }

            if(itemEvento.Estado == EstadoEvento.Pendiente && itemInvitado.Estado == EstadoInvitado.Registrado && itemInvitado.TipoInvitado==TipoInvitado.Adulto)
            {
                if (Utilidades.Correo.esCorreoValido(itemInvitado.Correo))
                {
                    //Notificacion al invitado
                    Utilidades.Correo.enviar(itemInvitado.Correo, "NOTIFICACION DE EVENTO", mensaje);
                }
                else {
                    Console.WriteLine($"El correo {itemInvitado.Correo} no es valido");
                }



                /*//Notificacion por SMS
                string accountSid = "ACce7b285fe81b6e5df246c0fe906b655b";//Cambiar aqui
                string authToken = "a14ea72b631325a0b331a0dbd197c472";//Cambiar aqui

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: mensaje,
                    from: new Twilio.Types.PhoneNumber("+12296196704"),//Cambiar aqui
                    to: new Twilio.Types.PhoneNumber(itemInvitado.Telefono)
                );

                Console.WriteLine(message.Sid);*/

            }

        }

    }
}