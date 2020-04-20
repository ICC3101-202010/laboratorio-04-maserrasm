using System;
using System.Collections.Generic;

// LAB 4 POO - Manuel Serra SM 
// 19.957.805-7 

namespace Lab4
{
    // Creo interface de computador central. 
    interface IcompCentralMaquinas
    {
        void encender(); void encenderMaquina(Maquina m);
        void apagar(); void apagarMaquina(Maquina m);
        void checkMemoria(); void checkMemoriaMaquina(Maquina m);
    }

    

    // La logica de esto esta en que al computador central le digo que aplique una operacion sobre
    // una maquina en especifico y este, mediante la interfaz, llama al metodo interno de aquella maquina,
    // efectuando la op. solicitada. De esta forma, el comp. central puede aplicar los metodos internos (apagar,
    // encender y reiniciar) de cada maquina, cosa que no seria posible sin interfaces.

    class ComputadorCentral : IcompCentralMaquinas
    {
        public void apagar()
        {
            throw new NotImplementedException();
        }

        public void apagarMaquina(Maquina m)
        {
            m.apagar();
        }

        public void encender()
        {
            throw new NotImplementedException();
        }

        public void encenderMaquina(Maquina m)
        {
            m.encender();
        }

        public void checkMemoria()
        {
            throw new NotImplementedException();
        }

        public void checkMemoriaMaquina(Maquina m)
        {
            m.checkMemoria();
        }
    }

    // Toda maquina tiene memoria; puede estar apagada o prendida y la memoria llena o no llena. 
    // La clase abstracta maquina hereda del comp. central los metodos de encendido, apagado y reinicio
    // de memoria. 

    //La memoria simplemente es un int, que va sumando una unidad cada vez que una maquina procesa una pieza.

    abstract class Maquina : IcompCentralMaquinas
    {
        public int memoria;
        public bool estaEncendida;
        public bool memoriaLlena;

        //Funcion que checkea si memoria de maquina esta llena o no. En caso de serlo retorna true. 
        public void checkMemoria()
        {
            if (memoria > 2)
            {
                Console.WriteLine("Limite de memoria excedido. Memoria reseteada a 0.");
                memoria = 0; 
            }
            else
            {
                Console.WriteLine("Memoria dentro da capacidad, vale: ", memoria);
            }
        }

        public void encender()
        {
            Console.WriteLine("Maquina encendida");
            estaEncendida = true;
        }

        public void apagar()
        {
            Console.WriteLine("Maquina apagada");
            estaEncendida = false;
        }
        public void reiniciar()
        {
            Console.WriteLine("Memoria reiniciada");
            memoria = 0; memoriaLlena = false;
        }

        public void verDatos()
        {
            Console.WriteLine("Estado encendida: "); Console.Write(estaEncendida); 
            Console.Write("Estado Memoria Llena: "); Console.Write(memoriaLlena);
        }

        public void encenderMaquina(Maquina m)
        {
            throw new NotImplementedException();
        }

        public void apagarMaquina(Maquina m)
        {
            throw new NotImplementedException();
        }

        public void reiniciarMaquina(Maquina m)
        {
            throw new NotImplementedException();
        }

        public void checkMemoriaMaquina(Maquina m)
        {
            throw new NotImplementedException();
        }
    }

    // En cuanto a las maquinas, cada tipo se encarga de un proceso distinto, cambiando el atributo bool
    // de la pieza de falso a verdadero (Ej. Empaque cambia atributo bool "empaquetado" de false a verdadero).
    // Dado que las piezas deben seguir un orden logico, las maquinas solo ejecutan la operacion si es que la pieza
    // ha pasado por los procesos anteriores correspondientes (Ej. La maquina de empaque, antes de cambiar el atributo
    // "empacado" de la pieza que esta procesando, chechea que los atributos "almacenado" y "ensamblado" de la pieza
    // sean true, de caso contario arroja error). 

    class Recepcion : Maquina
    {
        //Constructor.
        public Recepcion()
        {
            memoria = 0; estaEncendida = false; memoriaLlena = false;
        }
        public void recibir(Pieza P)
        {
            memoria += 1;
            P.recibida = true; Console.WriteLine("Pieza recibida con exito, memoria vale: ");
            Console.WriteLine(memoria);
        }
    }

    class Almacenamiento : Maquina
    {
        //Constructor.
        public Almacenamiento()
        {
            memoria = 0; estaEncendida = false; memoriaLlena = false;
        }
        public void almacenar(Pieza P)
        {
            if (P.recibida == true) 
            {
                memoria += 1;
                P.almacenada = true; Console.WriteLine("Pieza almacenada con exito, memoria vale: ");
                Console.WriteLine(memoria);
            }
        }
    }

    class Ensamblaje : Maquina
    {
        //Constructor.
        public Ensamblaje()
        {
            memoria = 0; estaEncendida = false; memoriaLlena = false;
        }
        public void ensamblar(Pieza P)
        {
            if (P.recibida == true && P.almacenada == true)
            {
                memoria += 1;
                P.ensablada = true; Console.WriteLine("Pieza ensamblada con exito, memoria vale: ");
                Console.WriteLine(memoria);
            }
        }
    }

    class Verificacion : Maquina
    {
        //Constructor.
        public Verificacion()
        {
            memoria = 0; estaEncendida = false; memoriaLlena = false;
        }
        public void verificar(Pieza P)
        {
            if (P.recibida == true && P.almacenada == true && P.ensablada == true)
            {
                memoria += 1;
                P.verificada = true; Console.WriteLine("Pieza verificada con exito, memoria vale: ");
                Console.WriteLine(memoria);
            }
        }
    }

    class Empaque : Maquina
    {
        //Constructor.
        public Empaque()
        {
            memoria = 0; estaEncendida = false; memoriaLlena = false;
        }
        public void empacar(Pieza P)
        {
            if (P.recibida == true && P.almacenada == true && P.ensablada == true && P.verificada == true)
            {
                memoria += 1;
                P.empaquetada = true; Console.WriteLine("Pieza empaquetada con exito, memoria vale: ");
                Console.WriteLine(memoria);
            }
        }
    }


    // Una pieza tiene 5 atributos booleanos que indican si esta ha pasado por cada uno de los procesos o no.
    class Pieza
    {
        public bool recibida; public bool almacenada;
        public bool ensablada; public bool verificada;
        public bool empaquetada;

        // Por default, una pieza parte con todos sus atributos en falso. 
        public Pieza()
        {
            recibida = false; almacenada = false;
            ensablada = false; verificada = false;
            empaquetada = false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Creo Comp. Central.
            ComputadorCentral cc = new ComputadorCentral();

            //Creo una maquina de cada tipo.

            Recepcion r1 = new Recepcion(); Almacenamiento a1 = new Almacenamiento();
            Ensamblaje e1 = new Ensamblaje(); Verificacion v1 = new Verificacion();
            Empaque emp1 = new Empaque();

            //Creo algunas piezas.

            Pieza p1 = new Pieza(); Pieza p2 = new Pieza();
            Pieza p3 = new Pieza(); Pieza p4 = new Pieza();

            //Vamos a simular un dia de operacion:

            //Primero parto dando la orden al comp. central de que encienda 
            //todas las maquinas en el orden correspondiente. 

            cc.encenderMaquina(r1); cc.encenderMaquina(a1); cc.encenderMaquina(e1);
            cc.encenderMaquina(v1); cc.encenderMaquina(emp1);

            //Procesamos la primera pieza.
            //Despues de realizar cada accion, ordeno a comp. central que
            //checkee memoria de cada maquina y la resetee si corresponde. 
            r1.recibir(p1); cc.checkMemoriaMaquina(r1);
            a1.almacenar(p1); cc.checkMemoriaMaquina(a1); 
            e1.ensamblar(p1); cc.checkMemoriaMaquina(e1);
            v1.verificar(p1); cc.checkMemoriaMaquina(v1);
            emp1.empacar(p1); cc.checkMemoriaMaquina(emp1);

            //La segunda. 
            r1.recibir(p2); cc.checkMemoriaMaquina(r1);
            a1.almacenar(p2); cc.checkMemoriaMaquina(a1);
            e1.ensamblar(p2); cc.checkMemoriaMaquina(e1);
            v1.verificar(p2); cc.checkMemoriaMaquina(v1);
            emp1.empacar(p2); cc.checkMemoriaMaquina(emp1);

            //.. la tercera.
            //Notemos que aca voy a exceder mi limite de memoria (cada maquina aguanta maximo 2 ops
            //y por ende se va a resetear x orden del computador central). 

            r1.recibir(p3); cc.checkMemoriaMaquina(r1);
            a1.almacenar(p3); cc.checkMemoriaMaquina(a1);
            e1.ensamblar(p3); cc.checkMemoriaMaquina(e1);
            v1.verificar(p3); cc.checkMemoriaMaquina(v1);
            emp1.empacar(p3); cc.checkMemoriaMaquina(emp1);

            //.. la cuarta. 

            r1.recibir(p4); cc.checkMemoriaMaquina(r1);
            a1.almacenar(p4); cc.checkMemoriaMaquina(a1);
            e1.ensamblar(p4); cc.checkMemoriaMaquina(e1);
            v1.verificar(p4); cc.checkMemoriaMaquina(v1);
            emp1.empacar(p4); cc.checkMemoriaMaquina(emp1);

            //Concluido el dia, apagamos las maquinas en el orden que corresponde. 

            cc.apagarMaquina(r1); cc.apagarMaquina(a1); cc.apagarMaquina(e1);
            cc.apagarMaquina(v1); cc.apagarMaquina(emp1);
        }
    }
}
