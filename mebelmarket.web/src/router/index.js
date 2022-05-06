import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: () => import("@/views/Home.vue")
  },
  {
    path: '/account/authentication/:formName',
    name: 'Authentication',
    component: () => import("@/views/Auth.vue"),
    props: true
  },

  {
    path: '/account/',
    name: 'Account',
    component: () => import("@/views/Account.vue")
  },

  {
    path: '/product/:uid',
    name: 'Product',
    component: () => import("@/views/Products/Index.vue"),
    props: true
  },
  {
    path: '/product/edit/:uid?',
    name: 'EditProduct',
    component: () => import("@/views/Products/Edit.vue"),
    props: true
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
