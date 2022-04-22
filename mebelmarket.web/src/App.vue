<template>
  <metainfo>
    <template v-slot:title="{ content }">{{ content }} - МебельМаркет</template>
  </metainfo>

  <component :is="layout">
    <router-view/>
  </component>
</template>

<style>
  @import "assets/css/animate-3.7.0.css";
  @import "assets/css/font-awesome-4.7.0.min.css";
  @import "assets/fonts/flat-icon/flaticon.css";
  @import "assets/css/bootstrap-4.1.3.min.css";
  @import "assets/css/nice-select.css";
  @import "assets/css/style.css";
</style>

<script>

  import { useMeta } from 'vue-meta'

  import MainLayout from '@/layouts/MainLayout'

  export default {

    setup () {
      useMeta({
        title: '',
        htmlAttrs: { lang: 'ru', amp: true }
      })
    },

    computed: {
      layout() {
        return "main-layout";
      }
    },

    components: { 
      MainLayout
    }
  }

</script>