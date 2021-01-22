import jade.core.Agent;
import java.util.HashMap;
import java.util.Random;
import java.util.concurrent.TimeUnit;

public class AverAgent extends Agent {

    private int id;
    private HashMap<Integer, Double> linkedAgents;
    private double value;

    @Override
    protected void setup() {
        this.id = Integer.parseInt(getAID().getLocalName());
        linkedAgents = new HashMap<>();
        for (int i = 0; i < 5; i++){
                linkedAgents.put(i + 1, 1.0);
        }

        Random rand = new Random();
        this.value =  rand.nextInt(10);
        addBehaviour(new FindBehaviour(this, TimeUnit.SECONDS.toMillis(1)));
        addBehaviour(new RecBehaviour(this));
    }

    public Integer getId() {
        return id;
    }

    public HashMap<Integer, Double> getLinkedAgents() {
        return linkedAgents;
    }

    public double getNumber() {
        return value;
    }

    public void setNumber(double number) {
        this.value = number;
    }
}