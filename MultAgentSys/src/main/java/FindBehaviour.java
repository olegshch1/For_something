import jade.core.behaviours.TickerBehaviour;

public class FindBehaviour extends TickerBehaviour{
    private final AverAgent agent;

    FindBehaviour(AverAgent agent, long period) {
        super(agent, period);
        this.setFixedPeriod(true);
        this.agent = agent;
    }

    @Override
    protected void onTick() {
        if (!agent.isCenter()) {
            agent.sendMessage();
        }

        if (agent.isTriggered()) {
            stop();
        }

        var msg = agent.blockingReceive();

        if (msg != null) {
            agent.proceedIncomingMessage(msg);

            if (agent.isCenter()) {
                System.out.println("Average has been found. Answer is " + agent.getValue());
                stop();
            }
        }
    }
}
