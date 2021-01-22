import jade.core.AID;
import jade.core.behaviours.TickerBehaviour;
import jade.lang.acl.ACLMessage;

import java.util.Collection;
import java.util.Random;

public class FindBehaviour extends TickerBehaviour {

    private final AverAgent agent;
    private int currentStep;
    Random rand = new Random();

    FindBehaviour(AverAgent agent, long period) {
        super(agent, period);
        this.setFixedPeriod(true);
        this.agent = agent;
        this.currentStep = 0;
    }

    @Override
    protected void onTick() {
        if (currentStep < App.MAX_STEPS) {
            //agent info.
            System.out.println(String.format("Tick = %d, agent = %d, value = %f", getTickCount(), this.agent.getId(), this.agent.getNumber()));

            if (!this.agent.getLinkedAgents().isEmpty()) {
                for (int receiver_id: this.agent.getLinkedAgents().keySet()) {
                    double x =  rand.nextDouble();
                    if(x>0.3) {
                        String content = Double.toString(this.agent.getNumber());
                        this.sendMessage(receiver_id, content);
                    }
                }
            }
            this.currentStep++;
        } else {
            this.stop();
        }
    }

    private void sendMessage(int id, String content) {
        if (this.agent.getId() != id) {
            ACLMessage msg = new ACLMessage(ACLMessage.INFORM);
            msg.setContent(content);
            AID dest = new AID(Integer.toString(id), AID.ISLOCALNAME);
            msg.addReceiver(dest);
            this.agent.send(msg);
        }
    }
}
