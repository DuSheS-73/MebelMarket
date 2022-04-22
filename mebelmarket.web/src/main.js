import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'

import ApiHelperMixin from './mixins/ApiHelperMixin'

import axios from 'axios'
import VueAxios from 'vue-axios'

import { createMetaManager } from 'vue-meta'

createApp(App)
	.use(store)
	.use(router)
	.use(VueAxios, axios)
	.use(createMetaManager())
	.mixin(ApiHelperMixin)
	.mount('#app')
