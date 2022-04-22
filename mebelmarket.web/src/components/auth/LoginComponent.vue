<template>
	<form>
    <div v-if="errors.length > 0" class="row px-2 my-3">
      <div class="col alert-danger">
        <p
          v-for="error in errors"
          class="m-0">
            {{ error }}
          </p>
      </div>
    </div>

    <div class="row mb-3">
      <div class="col">
        <label for="login">Телефон или E-mail<span class="text-danger">*</span></label>
        <input 
          type="text" 
          name="login" 
          id="login"
          class="form-control"
          v-model="login"
          >
      </div>
    </div>
    
    <div class="row mb-3">
      <div class="col">
        <div class="position-relative">
          <label for="password">Пароль <span class="text-danger">*</span></label>

          <input 
            :type="showPassword ? 'text' : 'password'" 
            name="password" 
            id="password"
            class="form-control pe-36"
            v-model="password"
            >

          <i class="fa password-eye" 
            :class="eyeClass" 
            @click="showPassword = !showPassword"></i>
        </div>
      </div>
    </div>
    
    <div class="row mb-3">
      <div class="col-auto mx-auto mt-3">
        <button type="submit" class="template-btn" @click.prevent="signIn">Войти</button>
      </div>
    </div>
  </form> 
</template>

<script>
  
  import { useMeta } from 'vue-meta'
  import axios from 'axios'

  export default {

    data() {
      return {
        login: "",
        password: "",
        showPassword: false,
        errors: []
      }
    },

    setup () {
      useMeta({
        title: 'Вход'
      })
    },

    computed: {
      eyeClass() {
        return this.showPassword ? "fa-eye" : "fa-eye-slash";
      }
    },

    methods: {

      signIn() {

        // Clear the errors
        this.errors.splice(0);

        if (this.login.split(' ').join('') == "" || this.password == "") {
          this.errors.push("Все поля обязательны к заполнению");
          return;
        }

        let url = this.createRoute("Account", "Login");
        let requestData = { login: this.login, password: this.password };

        axios
          .post(url, requestData, { headers: this.generateDeafaultHeaders() })
          .then(response => {
            console.log(response);
            localStorage.token = response.data.token;
          })
          .catch(error => {
            console.log(error);
            if (error.response) {
              this.errors = error.response.data.errors;
            }
          })
      }

    }
  }

</script>