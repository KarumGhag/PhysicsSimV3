using GlobalInfo;
using GameClass;
using ParticleClass;
using CollisionSystem;

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

    public virtual void Init()
    {

    }

    public virtual void Reset()
    {

    }
}