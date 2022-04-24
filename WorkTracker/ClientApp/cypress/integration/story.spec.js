describe("Story tests", () => {
  beforeEach(function(){
    cy.fixture("story").then(function(storyData){
      this.storyData = storyData;
    });
  });
  it("Create story with task", function() {
    cy.demo_login();
    cy.get(".button-inactive-primary").first().click();
    cy.contains("Create story");
    cy.get("input").first()
      .type(this.storyData.title);
    cy.get("textarea").first()
      .type(this.storyData.description);
    cy.get("input").last()
      .type(this.storyData.tasks[0].description);
    cy.contains("Save").click({ force: true });
    cy.contains("Test story title");
  });
  it("Edit story with task", function() {
    cy.intercept("/story/task/*").as("tasks");
    cy.demo_login();
    cy.contains("Test story title").click();
    cy.wait("@tasks");
    cy.get(".text-container").contains("Test story task 1")
      .click();
    cy.get("input").first()
      .type(" edit");
    cy.contains("Save").click({ force: true });
  });
  it("Change story state", function() {
    cy.intercept("/story/*").as("stories");
    cy.demo_login();
    cy.wait("@stories");
    cy.get(".story-container").first().parent()
      .drag(".state-column-container:nth-child(3) .state-column-content :nth-child(1)");
  });
  it("Delete story with task", () => {
    cy.demo_login();
    cy.contains("Test story title").click();
    cy.contains("Test story task 1");
    cy.contains("Delete").click({ force: true });
  });
});
