<template>
  <component :is="layout">
    <router-view/>
  </component>
</template>

<style>
  @import "assets/css/animate-3.7.0.css";
  @import "assets/css/font-awesome-4.7.0.min.css";
  @import "assets/fonts/flat-icon/flaticon.css";
  @import "assets/css/bootstrap-4.1.3.min.css";
  @import "assets/css/owl-carousel.min.css";
  @import "assets/css/nice-select.css";
  @import "assets/css/style.css";
</style>

<script>

  import MainLayout from '@/layouts/MainLayout'

  export default {

    computed: {
      layout() {
        return "main-layout";
      }
    },

    components: { 
      MainLayout
    }

    // mounted() {
    //   // let externalScriptSrcs = [
    //   //   "assets/js/vendor/jquery-2.2.4.min.js",
    //   //   "assets/js/vendor/bootstrap-4.1.3.min.js",
    //   //   "assets/js/vendor/wow.min.js",
    //   //   "assets/js/vendor/owl-carousel.min.js",
    //   //   "assets/js/vendor/jquery.nice-select.min.js",
    //   //   "assets/js/vendor/ion.rangeSlider.js",
    //   //   "assets/js/main.js"
    //   // ];

    //   // for (let i = 0; i < externalScriptSrcs.length; i++) {

    //   //   let externalScript = document.createElement('script');
    //   //   externalScript.setAttribute('src', externalScriptSrcs[i]);

    //   //   document.head.appendChild(externalScript);
    //   // }
    // }
  }

</script>