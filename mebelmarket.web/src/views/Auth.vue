<template>
  <section class="job-single-content section-padding">
    <div class="container">

      <div class="row">
        <div class="col-lg-8 mx-auto">
          <div class="main-content">
            <div class="single-content1">

              <div class="row px-4">

                <div v-if="formName == 'login'" class="col target-form selected-form">Вход</div>
                <router-link v-else :to="{name:'Authentication', params:{formName:'login'}}" class="col target-form hoverable">
                  <div>Вход</div>
                </router-link>

                <div v-if="formName == 'register'" class="col target-form selected-form">Регистрация</div>
                <router-link v-else :to="{name:'Authentication', params:{formName:'register'}}" class="col target-form hoverable">
                  <div>Регистрация</div>
                </router-link>

              </div>

            </div>
          </div>
        </div>
      </div>

      <div class="row mt-2">
        <div class="col-md-8 mx-auto px-4 py-2">
          <component :is="currentForm"></component>
        </div>
      </div>

    </div>
  </section>
</template>

<style>
  .target-form {
    cursor: pointer;
    text-align: center;
    border-bottom: 1px solid #bbb;
  }

  .selected-form {
    color: #ff9902;
    border-bottom: 1px solid #ff9902;
  }

  .hoverable {
    transition: .5s ease;
  }

  .hoverable:hover {
    color: rgba(255, 153, 2, .3);
    border-bottom: 1px solid rgba(255, 153, 2, .3);
  }

  .fa-eye {
    color: #ff9902;
  }

  .password-eye {
    cursor: pointer;
    position: absolute;
    right: 0;
    top: 50%;
    transform: translate3d(-50%, 50%, 0);
  }

  .pe-36 {
    padding-right: 36px !important;
  }

  .input-red,
  .input-red:focus {
    background: #f8d7da;
  }

  .input-green,
  .input-green:focus {
    background: #d1e7dd;
  }
</style>

<script>
  
  import LoginComponent from '@/components/auth/LoginComponent'
  import RegisterComponent from '@/components/auth/RegisterComponent'

  export default {

    props: {
      formName: { default: 'login', type: String }
    },

    components: { LoginComponent, RegisterComponent },

    computed: {
      currentForm() {
        return this.formName + '-component';
      }
    },

    methods: {
      
    }
  }

</script>