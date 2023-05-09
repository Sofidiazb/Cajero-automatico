using Clases;

class Program
{
    static void Main(string[] args)
    {
        var cliente = new Cliente("usuario1", "clave1");
        cliente.Nombre = "Juan";
        cliente.Apellido = "Pérez";
        cliente.LimiteCredito = 1000;

        var cuenta1 = new Cuenta(TipoCuenta.CajaAhorro, 1000, "1234");
        cuenta1.CobrarMantenimiento(50);
        Console.WriteLine($"Saldo después del mantenimiento: {cuenta1.Saldo}");


        var cuenta2 = new Cuenta(TipoCuenta.CuentaCorriente, 10000, "222222");
        cuenta2.CobrarMantenimiento(52);
        Console.WriteLine($"Saldo" +
            
            $" después del mantenimiento: {cuenta2.Saldo}");
        cliente.AgregarCuenta(cuenta2);

        Console.WriteLine($"Bienvenido, {cliente.Nombre} {cliente.Apellido}!");

        var opcion = 0;

        while (opcion != 6)

        {
            Console.WriteLine("¿Qué operación desea realizar?");
            Console.WriteLine("1. Consultar saldo");
            Console.WriteLine("2. Realizar depósito");
            Console.WriteLine("3. Realizar retiro");
            Console.WriteLine("4. Consultar movimientos");
            Console.WriteLine("5. Cantidad de cuentas asociadas");
            Console.WriteLine("6. Salir");

            var input = Console.ReadLine();
            opcion = int.Parse(input);

            switch (opcion)
            {
                case 1:
                    ConsultarSaldo(cliente);
                    break;
                case 2:
                    RealizarDeposito(cliente);
                    break;
                case 3:
                    RealizarRetiro(cliente);
                    break;
                case 4:
                    ConsultarMovimientos(cliente);
                    break;
                case 5:
                    Console.WriteLine($"El cliente tiene {cliente.CantidadCuentas} cuentas asociadas:");
                    foreach (var cuenta in cliente.ObtenerCuentas())
                    {
                        Console.WriteLine($"- {cuenta.Numero} ({cuenta.Tipo})");
                    }
                    break;
                case 6:
                    Console.WriteLine("Gracias por utilizar nuestro servicio. ¡Hasta luego!");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void ConsultarSaldo(Cliente cliente)
    {
        Console.WriteLine("¿De qué cuenta desea consultar el saldo?");
        var numeroCuenta = Console.ReadLine();

        try
        {
            var cuenta = cliente.ObtenerCuenta(numeroCuenta);
            Console.WriteLine($"El saldo de la cuenta {cuenta.Numero} es: ${cuenta.Saldo}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void RealizarDeposito(Cliente cliente)
    {
        Console.WriteLine("¿A qué cuenta desea realizar el depósito?");
        var numeroCuenta = Console.ReadLine();

        try
        {
            var cuenta = cliente.ObtenerCuenta(numeroCuenta);

            Console.WriteLine("Ingrese el monto a depositar:");
            var inputMonto = Console.ReadLine();
            var monto = double.Parse(inputMonto);

            cuenta.AplicarCredito(monto);

            Console.WriteLine($"El depósito de ${monto} fue realizado exitosamente en la cuenta {cuenta.Numero}.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void RealizarRetiro(Cliente cliente)
    {
        Console.WriteLine("¿De qué cuenta desea realizar el retiro?");
        var numeroCuenta = Console.ReadLine();

        try
        {
            var cuenta = cliente.ObtenerCuenta(numeroCuenta);

            Console.WriteLine("Ingrese el monto a retirar:");
            var inputMonto = Console.ReadLine();
            var monto = double.Parse(inputMonto);

            var exito = cuenta.AplicarDebito(monto);

            if (exito)
            {
                Console.WriteLine($"El retiro de ${monto} fue realizado exitosamente de la cuenta {cuenta.Numero}.");
            }
            else
            {
                Console.WriteLine($"El retiro de ${monto} no pudo ser realizado debido a fondos insuficientes en la cuenta {cuenta.Numero}.");
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (FormatException)
        {
            Console.WriteLine("Monto inválido. Por favor, ingrese un valor numérico.");
        }
    }

    private static void ConsultarMovimientos(Cliente cliente)
    {
        Console.WriteLine("¿De qué cuenta desea consultar los movimientos?");
        var numeroCuenta = Console.ReadLine();

        try
        {
            var cuenta = cliente.ObtenerCuenta(numeroCuenta);

            Console.WriteLine($"Movimientos de la cuenta {cuenta.Numero}:");
            foreach (var movimiento in cuenta.ObtenerMovimientos())
            {
                Console.WriteLine($"{movimiento.Fecha}: {movimiento.Tipo} por ${movimiento.Monto}");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

}