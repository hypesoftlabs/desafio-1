import Keycloak from "keycloak-js";

const keycloak = new Keycloak({
  url: "http://localhost:8080/",
  realm: "hypersoft-realm",
  clientId: "hypersoft-api",
});

export default keycloak;
