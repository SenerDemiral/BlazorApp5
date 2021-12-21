using Microsoft.AspNetCore.Components.Server.Circuits;

public class TrackingCircuitHandler : CircuitHandler
{
    private HashSet<Circuit> circuits = new();
    public event EventHandler CircuitsChanged;

    protected virtual void OnCircuitsChanged() => CircuitsChanged?.Invoke(this, EventArgs.Empty);
    public int ConnectedCircuits => circuits.Count;
    
    public override Task OnConnectionUpAsync(Circuit circuit,
        CancellationToken cancellationToken)
    {
        circuits.Add(circuit);
        OnCircuitsChanged();

        return base.OnConnectionUpAsync(circuit, cancellationToken);
        //return Task.CompletedTask;
    }

    public override Task OnConnectionDownAsync(Circuit circuit,
        CancellationToken cancellationToken)
    {
        circuits.Remove(circuit);
        OnCircuitsChanged();

        return base.OnConnectionDownAsync(circuit, cancellationToken);
        //return Task.CompletedTask;
    }

}

