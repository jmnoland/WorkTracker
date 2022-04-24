describe("Demo login test", () => {
  it("Login then logout", () => {
    cy.demo_login();
    cy.get(".title-start").should("have.text", "Work");
    cy.get(".title-end").should("have.text", "Tracker");
    cy.contains("Logout").click();
  });
});