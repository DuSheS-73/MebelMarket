<template>
	<header class="header-area single-page">
        <div class="header-top">
            <div class="container">
                <div class="row">
                    <div class="col-lg-2">
                        <div class="logo-area">
                            <a href="index.html"><img src="@/assets/images/logo-light.png" alt="logo"></a>
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="custom-navbar">
                            <span></span>
                            <span></span>
                            <span></span>
                        </div>  
                      <!--   <router-link
									:to="{name: 'Authentication', params: { returnUrl: $route.path }}"
									>
										<i class="action-button far" :class="accountClass"></i>
									</router-link> -->
                        <div class="main-menu main-menu-light">
                            <ul>
                                <li class="active"><router-link :to="{name:'Home'}">все объявления</router-link></li>
                                <li><a href="about.html">по категориям</a></li>
                                <li class="menu-btn">
                                    <router-link :to="{name:'Authentication', params:{formName:'register'}}" class="login">зарегистрироваться</router-link>
                                    <router-link :to="{name:'Authentication', params:{formName:'login'}}" class="template-btn">войти</router-link>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- <div class="page-title text-center">
            <div class="container">
                <div class="row">
                    <div class="col-md-6 offset-md-3">
                        <h2>Job Category</h2>
                        <p>There spirit beginning bearing the open at own every give appear in third you sawe two boys</p>
                    </div>
                </div>
            </div>
        </div> -->
    </header>
</template>

<script>
	
	export default {
	}

</script>