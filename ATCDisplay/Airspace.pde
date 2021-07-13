public class Airspace {
  
  private float angle = 0;
  private String name;
  
  Airspace(String name) {
    this.name = name;
  }
  
  public String getName() {
    return name;
  }
  
  public void setName(String name) {
    this.name = name;
  }
  
  public void showAirspace() {
    text(name, 5, 15);
    
    noStroke();
    fill(0, 0, 0, 70);
    rect(0, 0, width, height);
    stroke(0, 255, 0);
    strokeWeight(0.5);
    noFill();
    translate(width / 2, height / 2);
  
    //background(0, 0, 20);
  
    ellipse(0, 0, width - width * 0.1, height - height * 0.1);
  
    for(int i = 0; i < 360; i += 10) {
      float theta = radians(i);
      float alpha = radians(5);
      line(width / 3 * cos(theta), width / 3 * sin(theta), width / 3 * cos(theta + alpha), width / 3 * sin(theta + alpha));
      line(width / 4 * cos(theta), width / 4 * sin(theta), width / 4 * cos(theta + alpha), width / 4 * sin(theta + alpha));
      line(width / 6 * cos(theta), width / 6 * sin(theta), width / 6 * cos(theta + alpha), width / 6 * sin(theta + alpha));
      line(width / 12 * cos(theta), width / 12 * sin(theta), width / 12 * cos(theta + alpha), width / 12 * sin(theta + alpha));
    }
    fill(0, 255, 0);
    ellipse(0, 0, 10, 10);
  
    stroke(255, 100, 100);
    strokeWeight(1);
    line(-10, -30, -10, 10);
    line(-20, -15, 10, -15);
    line(15, -10, 15, 8);
    line(10, 12, 25, 12);
  
    strokeWeight(0.5);
    stroke(0, 255, 0);
    line(0, 0, (width - width * 0.1) / 2 * cos(angle), (width - width * 0.1) / 2 * sin(angle));
    if(angle <= TWO_PI) {
      angle += 0.01;
    } else { 
      angle = 0;
      //saveFrame("dispShot.png");
      //noLoop();
    }
  }
  
}
