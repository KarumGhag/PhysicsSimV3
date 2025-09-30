using GlobalInfo;
using GameClass;
using ParticleClass;

namespace SimulationClass;

public class Simulation
{
    public Game game;
    public Simulation(Game game)
    {
        this.game = game;
    }

    public virtual void Update(float deltaTime)
    {

    }
}