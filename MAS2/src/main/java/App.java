public class App {

    public static int NUMBER_OF_AGENTS = 5;
    public static int MAX_STEPS = 15;
    public static double noisemin = -0.1;
    public static double noisemax = 0.1;

    public static void main(String[] args) {
        MainController mc = new MainController(NUMBER_OF_AGENTS);
        mc.initAgents();
    }
}


