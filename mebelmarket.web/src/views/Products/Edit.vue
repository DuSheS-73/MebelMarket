<template>

	<div class="container mt-5">
		<div class="row">
			<div class="col-lg-12">
				
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
			        <label for="name">Наименование <span class="text-danger">*</span></label>
			        <input 
			          name="name" 
			          id="name"
			          class="form-control"
			          v-model="name"
			          >
			      </div>
			    </div>
			    
			    <div class="row mb-3">
			      <div class="col">
			        <div class="position-relative">
			          <label for="description">Описание <span class="text-danger">*</span></label>

			          <input 
			            name="description" 
			            id="description"
			            class="form-control pe-36"
			            v-model="description"
			            >
			        </div>
			      </div>
			    </div>
			    
			    <div class="row mb-3">
			      <div class="col">
			        <div class="position-relative">
			          <label for="price">Цена <span class="text-danger">*</span></label>

			          <input 
			            name="price" 
			            id="price"
			            class="form-control pe-36"
			            v-model="price"
			            >
			        </div>
			      </div>
			    </div>
			    

			    <div class="row mb-3">
			      <div class="col-auto mx-auto mt-3">
			        <button type="submit" class="template-btn" @click.prevent="updateProduct">Сохранить</button>
			      </div>
			    </div>
			  </form> 

			</div>
		</div>
	</div>
	
</template>

<script>

	import { useMeta } from 'vue-meta'
  import axios from 'axios'

	export default {

		setup () {
      useMeta({
        title: 'Добавить объявление'
      })
    },

		props: {
			uid: { type: String, reqired: false }
		},

		data() {
			return {
				name: "",
				description: "",
				price: 0,
				categoryUid: "",
				errors: []
			}
		},

		created() {
			if (this.uid != "")
				this.fetchProduct();
		},

		methods: {
			fetchProduct() {

			},

			updateProduct() {

				let request = axios;
				let data = { 
					uid: this.uid,
					name: this.name,
					description: this.description,
					categoryUid: this.categoryUid
			 	};

				if (this.uid == "") {
					let url = this.createRoute("Products", "Create");
					axios.post(url, data, { headers: this.generateDeafaultHeaders() });
				}
				else {
					let url = this.createRoute("Products", "Update")
					axios.put(url, data, { headers: this.generateDeafaultHeaders() });
				}
			}
		}
	}
</script>