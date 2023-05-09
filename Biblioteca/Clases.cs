namespace Clases;
public enum TipoCuenta
{
    CajaAhorro,
    CuentaCorriente,
    CuentaSueldo
}

public enum TipoMovimiento
{
    Debito,
    Credito
}

public class Cuenta
{
    public Cuenta(TipoCuenta tipoCuenta, double saldo, string numero)
    {
        Tipo = tipoCuenta;
        Saldo = saldo;
        Numero = numero;
        _movimientos = new List<Movimiento>();
    }

    public string Numero { get; }
    public TipoCuenta Tipo { get; }
    public double Saldo { get; private set; }
    private readonly List<Movimiento> _movimientos;

    public bool AplicarDebito(Double monto)
    {
        if (monto > Saldo)
        {
            return false;
        }

        Saldo -= monto;
        _movimientos.Add(new Movimiento(monto, TipoMovimiento.Debito));

        return true;
    }

    public void AplicarCredito(Double monto)
    {
        Saldo += monto;
        _movimientos.Add(new Movimiento(monto, TipoMovimiento.Credito));
    }

    public void CobrarMantenimiento(double monto)
    {
        if (Tipo != TipoCuenta.CuentaSueldo)
        {
            Saldo -= monto;
            _movimientos.Add(new Movimiento(monto, TipoMovimiento.Debito));
        }
    }


    public IReadOnlyCollection<Movimiento> ObtenerMovimientos() => _movimientos.AsReadOnly();
}

public class Cliente
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Usuario { get; }
    public string Clave { get; private set; }
    public double LimiteCredito { get; set; }
    private readonly List<Cuenta> _cuentas;

    public Cliente(string usuario, string clave)
    {
        Usuario = usuario;
        Clave = clave;
        _cuentas = new List<Cuenta>();
    }

    public int CantidadCuentas => _cuentas.Count;

    public Cuenta ObtenerCuenta(string numeroCuenta)
    {
        var cuenta = _cuentas.FirstOrDefault(c => c.Numero == numeroCuenta);
        if (cuenta == null)
        {
            throw new ArgumentException("La cuenta no existe.");
        }

        return cuenta;
    }

    public bool AgregarCuenta(Cuenta cuenta)
    {
        if (_cuentas.Any(c => c.Numero == cuenta.Numero))
        {
            return false;
        }

        _cuentas.Add(cuenta);
        return true;
    }

    public IReadOnlyCollection<Cuenta> ObtenerCuentas() => _cuentas.AsReadOnly();

    public void CambiarClave(string nuevaClave)
    {
        Clave = nuevaClave;
    }
}

public class Movimiento
{
    public Movimiento(double monto, TipoMovimiento tipo)
    {
        Monto = monto;
        Tipo = tipo;
        Fecha = DateTime.Now;
    }

    public double Monto { get; }
    public TipoMovimiento Tipo { get; }
    public DateTime Fecha { get; }
}