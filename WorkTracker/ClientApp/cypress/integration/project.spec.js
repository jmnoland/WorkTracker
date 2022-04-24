describe("Project tests", () => {
  beforeEach(function(){
    cy.fixture("project").then(function(projectData){
      this.projectData = projectData;
    });
    cy.intercept("/project").as("projects");
    cy.demo_login();
    cy.contains("Projects").click();
    cy.wait("@projects");
  });
  it("Create project", function() {
    cy.contains("Create").click();
    cy.get("input").first()
      .type(this.projectData.name);
    cy.get("input").last()
      .type(this.projectData.description);
    cy.get("button").eq(1).click();
    cy.contains("Test project").should("exist");
  });
  it("Edit project", function() {
    cy.contains("Test project").click();
    cy.get("button").eq(3).click();
    cy.get("input").last()
      .type(" update");
    cy.get("button").eq(2).click();
    cy.contains("Cancel").click();
    cy.contains("Test project").click();
    cy.contains(this.projectData.description + " update")
      .should("exist");
  });
  it("Complete project", function() {
    cy.contains("Test project").click();
    cy.get("button").eq(2).click();
    cy.contains("Test project").click();
    cy.contains("Completed at").should("exist");
  });
  it("Delete project", () => {
    cy.contains("Test project").click();
    cy.get("button").eq(1).click();
    cy.contains("Test project").should('not.exist');
  });
});
