export default {
  methods: {
    createRoute(controller, action) { 
      const domain = "https://localhost:44344";
      const version = "v1";

      return domain + "/api/" + version + "/" + controller + "/" + action;
    },

    generateDeafaultHeaders() {
      return {
        //ApiKey: "EFCA418A-271E-4DF8-8F05-04D8350BD8EC"
        Authorization: "Bearer " + localStorage.token
      }
    }
  }
}