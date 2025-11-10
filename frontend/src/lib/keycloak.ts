// src/keycloak.ts
import Keycloak from "keycloak-js";

const keycloak = new Keycloak({
  url: "http://localhost/auth",   
  realm: "shop-realm",            
  clientId: "shop-frontend",      
});

export default keycloak;
