import jade.core.AID;
import jade.core.Agent;
import jade.lang.acl.ACLMessage;

import java.security.InvalidParameterException;
import java.util.concurrent.TimeUnit;

public class AverAgent extends Agent {
    private double value;
    private AID receiverAID;
    private boolean isPending;
    private boolean isTriggered;
    private int leftReceived;
    private int totalValues = 1;

    public boolean isTriggered() {
        return isTriggered;
    }

    public boolean isCenter() {
        return Integer.parseInt(getAID().getLocalName()) == 0;
    }

    public double getValue() {
        return value;
    }

    public void sendMessage() {
        if (isPending && !isTriggered) {
            ACLMessage newMes = new ACLMessage(ACLMessage.INFORM);
            newMes.addReceiver(receiverAID);
            newMes.setContent(value + ";" + totalValues);

            try {
                send(newMes);
            }
            finally {
                isPending = false;
                isTriggered = true;
            }
        }
    }

    public void proceedIncomingMessage(ACLMessage msg) {
        var args = msg.getContent().split(";");

        var msgDoubleValue = Double.parseDouble(args[0]);
        var msgTotalValues = Integer.parseInt(args[1]);

        if (isCenter()) {
            value = msgDoubleValue / msgTotalValues;
            return;
        }
        value = value + msgDoubleValue;
        totalValues += msgTotalValues;
        leftReceived--;

        if (leftReceived == 0) {
            isPending = true;
        }
    }

    @Override
    protected void setup() {
        Object[] args = getArguments();

        if (args != null && args.length > 0) {
            if (args.length != 4) {
                throw new InvalidParameterException("Invalid parameters for agent setup");
            }

            value = Double.parseDouble(args[0].toString());
            var numberOfAgents = Integer.parseInt(args[1].toString()) - 1;
            var id = Integer.parseInt(getAID().getLocalName());
            isPending = id % 2 == 1 && id != numberOfAgents;
            receiverAID = new AID(args[2].toString(), AID.ISLOCALNAME);
            leftReceived = Integer.parseInt(args[3].toString());
        }
        else {
            throw new InvalidParameterException("Invalid neighbours param");
        }

        addBehaviour(new FindBehaviour(this, TimeUnit.SECONDS.toMillis(1)));
    }

}
