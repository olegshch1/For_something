import jade.core.behaviours.CyclicBehaviour;
import jade.lang.acl.ACLMessage;

import java.util.HashMap;
import java.util.Map;
import java.util.Random;

public class RecBehaviour extends CyclicBehaviour {
    private final AverAgent agent;

    RecBehaviour(AverAgent agent) {
        super(agent);
        this.agent = agent;
    }

    @Override
    public void action() {
        ACLMessage msg = this.agent.receive();
        if (msg!=null)
            replyToMessage(msg);
        else {
            block();
        }
    }

    public void replyToMessage(ACLMessage msg) {
        String content = msg.getContent();
        String sender_name = msg.getSender().getName();
        int sender_id = Integer.parseInt(sender_name.substring(0, sender_name.indexOf("@")));

        // Replying to info message (value) with delta.
        if (msg.getPerformative() == ACLMessage.INFORM) {
            ACLMessage reply = msg.createReply();
            reply.setPerformative(ACLMessage.CONFIRM);
            double valueNeighbour = Double.parseDouble(content);
            double delta = (valueNeighbour - this.agent.getNumber())*0.1;
            double newValue = this.agent.getNumber() + delta;
            this.agent.setNumber(newValue);
            double randomNoise = App.noisemin + (App.noisemax - App.noisemin) * new Random().nextDouble();
            reply.setContent(String.format("%f", -delta + randomNoise));
            this.agent.send(reply);
        }

        if (msg.getPerformative() == ACLMessage.CONFIRM) {
            System.out.println(String.format("Agent with ID %d received INFORM message from Agent with ID %d", this.agent.getId(), sender_id));
            double delta = Double.parseDouble(content);
            double newValue = this.agent.getNumber() + delta;
            this.agent.setNumber(newValue);
        }
    }

}
