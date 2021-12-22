using Microsoft.AspNetCore.Components.Server.Circuits;

public class TrackingCircuitHandler : CircuitHandler
{
    private HashSet<Circuit> circuits = new();
    //public event EventHandler CircuitsChanged;
    //public event EventHandler<string> CircuitsChanged;
    public Action? CircuitsChanged;

    //protected virtual void OnCircuitsChanged() => CircuitsChanged?.Invoke(this, EventArgs.Empty);
    //protected virtual void OnCircuitsChanged(string id) => CircuitsChanged?.Invoke(this, id);
    protected virtual void OnCircuitsChanged() => CircuitsChanged?.Invoke();
    public int ConnectedCircuits => circuits.Count;

    public override Task OnConnectionUpAsync(Circuit circuit,
        CancellationToken cancellationToken)
    {
        circuits.Add(circuit);
        OnCircuitsChanged();
        //OnCircuitsChanged(circuit.Id);
        Console.WriteLine($"Circuit {circuit.Id} connected");

        return base.OnConnectionUpAsync(circuit, cancellationToken);
        //return Task.CompletedTask;
    }

    public override Task OnConnectionDownAsync(Circuit circuit,
        CancellationToken cancellationToken)
    {
        circuits.Remove(circuit);
        OnCircuitsChanged();
        //OnCircuitsChanged(circuit.Id);

        return base.OnConnectionDownAsync(circuit, cancellationToken);
        //return Task.CompletedTask;
    }

}

