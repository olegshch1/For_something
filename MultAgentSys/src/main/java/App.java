public class App {
    public static void main(String[] args){
        var params = ParamsReader.getParams("params.txt");
        MainController mc = new MainController(params);
        mc.initAgents();
    }
}
