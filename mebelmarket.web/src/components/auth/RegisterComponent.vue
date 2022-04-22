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
				<label for="userName">Логин <span class="text-danger">*</span></label>
				<input 
					type="text" 
					name="userName" 
					id="userName"
					class="form-control"
					v-model="userName"
					>
			</div>
		</div>

		<div class="row mb-3">
			<div class="col">
				<label for="email">E-mail <span class="text-danger">*</span></label>
				<input 
					type="text" 
					name="email" 
					id="email"
					class="form-control"
					v-model="email"
					placeholder="example@email.com" 
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
						:class="passwordInputClass"
						v-model="password"
						placeholder="Не менее 8 символов" 
						>

					<i class="far password-eye" 
						:class="eyeClass" 
						@click="showPassword = !showPassword"></i>
				</div>
			</div>
		</div>

		<div class="row mb-3">
			<div class="col">
				<div class="position-relative">
					<label for="passwordConfirmation">
						Подтверждение пароля <span class="text-danger">*</span>
					</label>
					
					<input 
						:type="showPassword ? 'text' : 'password'" 
						name="passwordConfirmation" 
						id="passwordConfirmation"
						class="form-control pe-36"
						:class="passwordConfirmationInputClass"
						v-model="passwordConfirmation"
						>

					<i class="far password-eye" 
						:class="eyeClass" 
						@click="showPassword = !showPassword"></i>
				</div>
			</div>
		</div>
		
		<div class="row mb-3">
			<div class="col-auto mx-auto mt-3">
				<button 
					type="submit" 
					class="template-btn" 
					@click.prevent="register" 
					:disabled="buttonDisabled"
					>
					Зарегистрироваться
				</button>
			</div>
		</div>
	</form>	
</template>

<script>

	import { useMeta } from 'vue-meta'
	import axios from 'axios'

	export default {
		name: 'RegisterComponent',

		data() {
			return {
				email: "",
				userName: "",
				password: "",
				passwordConfirmation: "",
				errors: [],
				showPassword: false
			}
		},

		setup () {
      useMeta({
        title: 'Регистрация'
      })
    },

		computed: {
			eyeClass() {
				return this.showPassword ? "fa-eye" : "fa-eye-slash";
			},

			passwordInputClass() {
				if (this.password.length == 0) {
					return '';
				}
				else if (this.password.length >= 8) {
					return 'input-green';
				}
				else {
					return 'input-red';
				}
			},

			passwordConfirmationInputClass() {

				if (this.passwordConfirmation.length == 0) {
					return ''
				}
				else if (
					this.passwordConfirmation.length > 0 && 
					this.passwordConfirmation !== this.password
				) {
					return 'input-red';
				}
				else {
					return 'input-green';
				}
			},

			buttonDisabled() {
				return this.password !== this.passwordConfirmation ||
							 this.password.length < 8;
			}
		},

		methods: {
			register() {

				this.errors.splice(0);

				if (this.password !== this.passwordConfirmation) {
					this.errors.push("Пароли не совпадают");
					return;
				}

				if (
					this.email.split(' ').join('') == "" ||
					this.userName.split(' ').join('') == "" ||
					this.password == "" ||
					this.passwordConfirmation == ""
				) {
					this.errors.push("Заполните все поля");
					return;
				}

				let registerRequest = {
					email: this.email, 
					userName: this.userName, 
					password: this.password
				};

				let url = this.createRoute("Identity", "Register");

				axios
				.post(url, registerRequest, { headers: this.generateHeaders() })
				.then(response => {
					localStorage.token = response.data.token;
					localStorage.refreshToken = response.data.refreshToken;
					localStorage.authenticated = true;
					
					this.$router.push({path: this.$parent.returnUrl ?? "/articles/main/page/1"});
				})
				.catch(error => {
					if (error.response) {
						this.errors = error.response.data.errors;
          }
				})
			}
		}
	}
</script>